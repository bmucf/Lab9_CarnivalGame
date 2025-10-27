using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class TransformSaveData
{
    public Vector3Data playerPosition;
    public List<FishSaveEntry> fishEntries = new List<FishSaveEntry>();
}

[System.Serializable]
public struct Vector3Data
{
    public float x, y, z;
    public Vector3Data(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}

[System.Serializable]
public struct FishSaveEntry
{
    public string fishType;
    public Vector3Data position;

    public FishSaveEntry(string typeName, Vector3 pos)
    {
        fishType = typeName;
        position = new Vector3Data(pos);
    }
}

public class TransformSaver : MonoBehaviour, ISaveable
{
    [Header("References (assign in Inspector)")]
    [SerializeField] private Transform player;
    [SerializeField] private FishSpawner fishSpawner;    // required to re-create fish via fishDirector
    [SerializeField] private ScoreManager scoreManager;  // optional, only for context

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "locations.json");
    }

    public void Save()
    {
        TransformSaveData data = new TransformSaveData();

        // player
        if (player != null)
            data.playerPosition = new Vector3Data(player.position);

        // collect ALL live Fish components (modern API)
        Fish[] allFish = Object.FindObjectsByType<Fish>(FindObjectsSortMode.None);
        foreach (var fish in allFish)
        {
            // ensure fish has a public fishType field/property
            string typeName = fish.fishType.ToString();
            data.fishEntries.Add(new FishSaveEntry(typeName, fish.transform.position));
        }

        // write JSON
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log($"[TransformSaver] Saved {data.fishEntries.Count} fish + player to {savePath}");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("[TransformSaver] No locations.json found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        TransformSaveData data = JsonUtility.FromJson<TransformSaveData>(json);

        // restore player
        if (player != null)
            player.position = data.playerPosition.ToVector3();

        // pause spawner so it won't add new random fish while we rebuild
        bool spawnerWasEnabled = false;
        if (fishSpawner != null)
        {
            spawnerWasEnabled = fishSpawner.enabled;
            fishSpawner.enabled = false;
        }

        // destroy existing runtime fish
        foreach (var f in Object.FindObjectsByType<Fish>(FindObjectsSortMode.None))
        {
            Destroy(f.gameObject);
        }

        // recreate saved fish using your FishDirector via FishSpawner
        if (fishSpawner != null && fishSpawner.fishDirector != null)
        {
            foreach (var entry in data.fishEntries)
            {
                if (System.Enum.TryParse(entry.fishType, out FishType ft))
                {
                    Fish newFish = fishSpawner.fishDirector.ConstructFish(ft, scoreManager);
                    // set position AFTER construction (some directors set up local transforms)
                    newFish.transform.position = entry.position.ToVector3();
                }
                else
                {
                    Debug.LogWarning($"[TransformSaver] Unknown FishType '{entry.fishType}' in save file.");
                }
            }
        }
        else
        {
            // Fallback: If no spawner/director assigned, create placeholder GameObjects tagged as Fish
            foreach (var entry in data.fishEntries)
            {
                GameObject go = new GameObject("Fish");
                go.tag = "Fish";
                go.transform.position = entry.position.ToVector3();
            }
            Debug.LogWarning("[TransformSaver] No FishSpawner/FishDirector assigned — created placeholder fish GameObjects instead.");
        }

        // restore spawner enabled state
        if (fishSpawner != null)
            fishSpawner.enabled = spawnerWasEnabled;

        Debug.Log($"[TransformSaver] Loaded {data.fishEntries.Count} fish + player from {savePath}");
    }
}

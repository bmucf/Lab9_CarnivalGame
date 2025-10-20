using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    public FishDirector fishDirector;

    [Header("Spawn Rates per Fish Type (seconds between spawns)")]
    public float redSpawnRate = 2f;
    public float orangeSpawnRate = 3f;
    public float greenSpawnRate = 4f;
    public float blueSpawnRate = 5f;
    public float pinkSpawnRate = 6f;
    public float brownSpawnRate = 7f;

    private Dictionary<FishType, float> spawnTimers;
    private Dictionary<FishType, float> spawnRates;

    private void Start()
    {
        spawnTimers = new Dictionary<FishType, float>();
        spawnRates = new Dictionary<FishType, float>
        {
            { FishType.Red, redSpawnRate },
            { FishType.Orange, orangeSpawnRate },
            { FishType.Green, greenSpawnRate },
            { FishType.Blue, blueSpawnRate },
            { FishType.Pink, pinkSpawnRate },
            { FishType.Brown, brownSpawnRate }
        };

        // Initialize all timers to 0 so first wave spawns immediately
        foreach (FishType type in System.Enum.GetValues(typeof(FishType)))
            spawnTimers[type] = 0f;
    }

    private void Update()
    {
        // Count up timers and spawn when they hit their rate
        foreach (FishType type in System.Enum.GetValues(typeof(FishType)))
        {
            spawnTimers[type] += Time.deltaTime;

            if (spawnTimers[type] >= spawnRates[type])
            {
                SpawnFish(type);
                spawnTimers[type] = 0f; // reset timer
            }
        }
    }

    private void SpawnFish(FishType type)
    {
        Fish fish = fishDirector.ConstructFish(type);
        Debug.Log($"Spawned {type} fish at {Time.time:F1}s");
    }
}

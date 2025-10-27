using UnityEngine;
using System.Linq;

public class SaveLoadController : MonoBehaviour
{
    private ISaveable[] saveables;

    void Awake()
    {
        saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ISaveable>()
            .ToArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (var s in saveables)
                s.Save();
            Debug.Log("[SaveLoadController] Saved all ISaveables.");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (var s in saveables)
                s.Load();
            Debug.Log("[SaveLoadController] Loaded all ISaveables.");
        }
    }
}

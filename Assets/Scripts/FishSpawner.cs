using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("Script is running");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Key was pressed");

            Fish newFish = new FishDirector().ConstructRedFish();
        }
    }
}

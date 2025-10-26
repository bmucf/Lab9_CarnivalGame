using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int preWarm = 10;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < preWarm; i++)
        {
            var go = Instantiate(bulletPrefab);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(bulletPrefab);
        }

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        // Reset bullet state if necessary
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
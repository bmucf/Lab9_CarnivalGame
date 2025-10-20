using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 30f;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        // Always move right
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // Destroy after lifetime expires
        if (Time.time - spawnTime > lifetime)
            Destroy(gameObject);
    }
}

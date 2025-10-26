using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 30f;
    private float timer = 0f;
    private Fish fish;

    private void Awake()
    {
        fish = GetComponent<Fish>();
        if (fish != null)
        {
            moveSpeed = fish.speed;
            lifetime = fish.lifetime;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
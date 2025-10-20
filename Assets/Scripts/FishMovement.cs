using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 30f;
    private float timer = 0f;

    private void Update()
    {
        // Move to the right continuously
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // Lifetime countdown
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}

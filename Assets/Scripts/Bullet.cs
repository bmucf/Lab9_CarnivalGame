using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1.5f;
    private float timer;
    private ObjectPool ownerPool;

    public void SetOwnerPool(ObjectPool pool)
    {
        ownerPool = pool;
    }

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifeTime)
            ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Target>() ?? other.GetComponentInParent<Target>();
        if (target != null)
        {
            target.OnHit();
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (ownerPool != null)
            ownerPool.ReturnObject(gameObject);
        else
            Destroy(gameObject);
    }
}
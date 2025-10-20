using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ObjectPool bulletPool;
    public float moveSpeed = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, -4.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = transform.position + transform.up * 0.5f;
        
        StartCoroutine(DeactivateBullet(bullet));
    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        bulletPool.ReturnObject(bullet);
    }
}

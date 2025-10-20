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
        Debug.Log("PlayerMovement Update is running");

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            Debug.Log("Moving Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
            Debug.Log("Moving Right");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Debug.Log("Shot");
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = transform.position + transform.up * 0.5f;

        var b = bullet.GetComponent<Bullet>();
        b.SetOwnerPool(bulletPool); // if you're using pooling logic
        b.speed = 10f;

        Debug.Log("Bullet fired!");
    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        bulletPool.ReturnObject(bullet);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public ScoreManager scoreManager;

    private Target currentTarget;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            spawnPos.z = 0;

            GameObject targetGO = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
            currentTarget = targetGO.GetComponent<Target>();

            if (currentTarget != null)
                currentTarget.RegisterObserver(scoreManager);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            clickPos.z = 0;

            Collider2D hit = Physics2D.OverlapPoint(clickPos);
            if (hit != null && hit.GetComponent<Target>() == currentTarget)
            {
                currentTarget.OnHit();
                Destroy(currentTarget.gameObject);
                currentTarget = null;
            }
        }
    }
}
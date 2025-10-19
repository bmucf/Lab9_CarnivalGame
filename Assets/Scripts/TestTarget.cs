using UnityEngine;

public class TestTarget : Target
{
    public override int pointValue => 100;


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public override void OnHit()
    {
        base.OnHit();

        // TODO: Change fish to bones, maybe add a hit sound
    }

}

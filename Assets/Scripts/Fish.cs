using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Fish : MonoBehaviour
{
    public string speciesName;
    public int pointValue;
    public int size;
    public int speed;
    public float lifetime;

    public Sprite sprite;
}

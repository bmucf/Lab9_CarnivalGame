using TMPro;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Fish() { }

    public float size = 1;
    public float speed = 1;
    public float pointValue = 10;
    public float lifetime = 30;
    public FishType fishType = FishType.Orange;
    public SpriteRenderer spriteRenderer;

    public class Builder
    {
        Fish fish = new GameObject("Fish").AddComponent<Fish>();

        public Builder WithSize(float _size)
        {
            fish.size = _size;
            fish.transform.localScale = Vector3.one * _size;
            return this;
        }

        public Builder WithSpeed(float _speed)
        {
            fish.speed = _speed;
            return this;
        }

        public Builder WithPointValue(float _pointValue)
        {
            fish.pointValue = _pointValue;
            return this;
        }

        public Builder WithLifetime(float _lifetime)
        {
            fish.lifetime = _lifetime;
            return this;
        }

        public Builder WithFishType(FishType _fishType, Sprite _sprite)
        {
            fish.fishType = _fishType;
            if (fish.spriteRenderer == null)
            {
                fish.spriteRenderer = fish.gameObject.AddComponent<SpriteRenderer>();
            }
            fish.spriteRenderer.sprite = _sprite;
            return this;
        }

        public Fish Build()
        {
            return fish;
        }
    }
}

public enum FishType { Red, Orange, Green, Blue, Pink, Brown }

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
    public CapsuleCollider2D capsuleCollider;

    public class Builder
    {
        private Fish fish;

        public Builder()
        {
            fish = new GameObject("Fish").AddComponent<Fish>();
        }

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

            // SpriteRenderer setup
            if (fish.spriteRenderer == null)
                fish.spriteRenderer = fish.gameObject.AddComponent<SpriteRenderer>();

            if (_sprite != null)
                fish.spriteRenderer.sprite = _sprite;
            else
                Debug.LogWarning($"No sprite assigned for {_fishType} fish!");

            // Collider setup
            if (fish.capsuleCollider == null)
                fish.capsuleCollider = fish.gameObject.AddComponent<CapsuleCollider2D>();

            // Configure the collider
            fish.capsuleCollider.direction = CapsuleDirection2D.Vertical;
            fish.capsuleCollider.isTrigger = true; // optional, depends on gameplay
            fish.capsuleCollider.size = new Vector2(0.8f, 1.2f); // adjust for visuals
            fish.capsuleCollider.offset = Vector2.zero;

            return this;
        }

        public Builder WithMovement()
        {
            FishMovement move = fish.gameObject.AddComponent<FishMovement>();
            move.moveSpeed = fish.speed;
            move.lifetime = fish.lifetime;
            return this;
        }

        public Fish Build()
        {
            return fish;
        }
    }
}

public enum FishType { Red, Orange, Green, Blue, Pink, Brown }

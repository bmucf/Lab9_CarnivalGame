using UnityEditor.Build.Content;
using UnityEngine;

public class Fish : Target
{
    // remove private constructor

    private float _speed = 1f;
    private int _pointValue = 10;

    public override float speed => _speed;
    public override int pointValue => _pointValue;

    public float size = 1f;
    public float lifetime = 20f;
    public FishType fishType = FishType.Orange;

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        // Cache components if present, else add them when needed later
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void InitializeAfterBuild()
    {
        transform.localScale = Vector3.one * size;

        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        if (capsuleCollider == null)
            capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();

        capsuleCollider.direction = CapsuleDirection2D.Vertical;
        capsuleCollider.isTrigger = true;
        capsuleCollider.size = new Vector2(1.6f, 1.6f);

        // Add/ensure movement exists and matches configured values
        var move = GetComponent<FishMovement>();
        if (move == null)
            move = gameObject.AddComponent<FishMovement>();

        move.moveSpeed = speed;
        move.lifetime = lifetime;
    }

    public void SetSpeed(float value) => _speed = value;
    public void SetPointValue(int value) => _pointValue = value;

    public override void OnHit()
    {
        NotifyObservers();
        var sr = spriteRenderer ?? GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.red;
        // Consider playing an effect or disabling visuals here
    }

    // Builder remains but must call InitializeAfterBuild() in Build()
    public class Builder
    {
        private Fish fish;

        public Builder()
        {
            var go = new GameObject("Fish");
            fish = go.AddComponent<Fish>();
        }

        public Builder WithSize(float _size)
        {
            fish.size = _size;
            return this;
        }

        public Builder WithSpeed(float _speed)
        {
            fish.SetSpeed(_speed);
            return this;
        }

        public Builder WithPointValue(int _pointValue)
        {
            fish.SetPointValue(_pointValue);
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
                fish.spriteRenderer = fish.gameObject.AddComponent<SpriteRenderer>();

            if (_sprite != null)
                fish.spriteRenderer.sprite = _sprite;
            else
                Debug.LogWarning($"No sprite assigned for {_fishType} fish!");

            return this;
        }

        // Defer movement component setup to InitializeAfterBuild
        public Fish Build()
        {
            fish.InitializeAfterBuild();
            return fish;
        }
    }
}
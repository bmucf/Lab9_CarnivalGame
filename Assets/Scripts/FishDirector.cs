using UnityEngine;
using System.Collections.Generic;

public class FishDirector : MonoBehaviour
{
    [Header("Fish Sprites (match order with FishType)")]
    public Sprite[] fishSprites; // 0=Red, 1=Orange, etc.

    private Dictionary<FishType, Sprite> spriteMap;

    private void Awake()
    {
        spriteMap = new Dictionary<FishType, Sprite>();
        FishType[] types = (FishType[])System.Enum.GetValues(typeof(FishType));
        for (int i = 0; i < types.Length && i < fishSprites.Length; i++)
            spriteMap[types[i]] = fishSprites[i];
    }

    private Sprite GetSprite(FishType type)
    {
        if (spriteMap.TryGetValue(type, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"No sprite assigned for FishType: {type}");
        return null;
    }

    public Fish ConstructFish(FishType type)
    {
        Sprite sprite = GetSprite(type);
        float speed = GetSpeedForType(type);
        float height = GetHeightForType(type);

        Fish fish = new Fish.Builder()
            .WithFishType(type, sprite)
            .WithSize(1f)
            .WithSpeed(speed)
            .WithPointValue(Random.Range(5, 20))
            .WithLifetime(30)
            .WithMovement()
            .Build();

        // Position fish: fixed X = -10, Y depends on type
        fish.transform.position = new Vector3(-10f, height, 0f);
        return fish;
    }

    private float GetSpeedForType(FishType type)
    {
        switch (type)
        {
            case FishType.Red: return 3.5f;
            case FishType.Orange: return 3f;
            case FishType.Green: return 2.5f;
            case FishType.Blue: return 2f;
            case FishType.Pink: return 1.5f;
            case FishType.Brown: return 1f;
            default: return 2f;
        }
    }

    private float GetHeightForType(FishType type)
    {
        switch (type)
        {
            case FishType.Red: return 3f;
            case FishType.Orange: return 2f;
            case FishType.Green: return 1f;
            case FishType.Blue: return 0f;
            case FishType.Pink: return -1f;
            case FishType.Brown: return -2f;
            default: return 0f;
        }
    }
}

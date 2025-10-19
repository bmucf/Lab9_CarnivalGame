using System.Collections.Generic;
using UnityEngine;

public class FishBuilder : MonoBehaviour
{
    public enum FishSprites
    {
        Orange,
        Green,
        Red,
        Blue,
        Pink,
        Puff,
        Dead
    }

    public FishSprites selectedSprite;

    Dictionary<FishSprites, string> fishSpritePaths = new Dictionary<FishSprites, string>()
    {
        { FishSprites.Orange, "fish_orange_outline" },
        { FishSprites.Green, "fish_green_outline" },
        { FishSprites.Red, "fish_red_outline" },
        { FishSprites.Blue, "fish_blue_outline" },
        { FishSprites.Pink, "fish_pink_outline" },
        { FishSprites.Puff, "fish_brown_outline" },
        { FishSprites.Dead,  "fish_skeleton_outline"}
    };


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject fishObject = new GameObject("Fish");


            Fish fish = fishObject.AddComponent<Fish>();
            SpriteRenderer fishAppearance = fishObject.AddComponent<SpriteRenderer>();

            fish.speciesName = "Placeholder Name";
            fish.pointValue = 10;
            fish.speed = 10;
            fish.lifetime = 10f;

            string spritePath = fishSpritePaths[selectedSprite];
            Sprite fishSprite = Resources.Load<Sprite>(spritePath);

            if (fishSprite != null)
            {
                fishAppearance.sprite = fishSprite;
            }
            else
            {
                Debug.LogError("Sprite not found at path: " +  spritePath);
            }
        }
    }
}

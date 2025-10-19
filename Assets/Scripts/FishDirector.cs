using UnityEngine;

public class FishDirector : MonoBehaviour
{
    public Sprite redFishSprite;
    public Sprite orangeFishSprite;
    public Sprite greenFishSprite;
    public Sprite blueFishSprite;
    public Sprite pinkFishSprite;
    public Sprite brownFishSprite;

    private Fish.Builder builder = new Fish.Builder();

    public Fish ConstructOrangeFish()
    {
        return builder.Build();
    }

    public Fish ConstructRedFish()
    {
        return builder.Build();
    }

}

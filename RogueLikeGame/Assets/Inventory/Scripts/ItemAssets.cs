using UnityEngine;

public class ItemAssets : MonoBehaviour {

    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }


    public Transform pfItemWorld;

    public Sprite rollingPinSprite;
    public Sprite cookingPotSprite;
    public Sprite ladleSprite;
    public Sprite cookingKnifeSprite;
    public Sprite redMeatSprite;
    public Sprite turkeySprite;
    public Sprite chickenSprite;
    public Sprite baconSprite;
    public Sprite porkSprite;
    public Sprite appleSprite;
    public Sprite bananaSprite;
    public Sprite grapeSprite;
    public Sprite lemonSprite;
    public Sprite limeSprite;
    public Sprite orangeSprite;
    public Sprite pearSprite;
    public Sprite strawberrySprite;
    public Sprite carrotSprite;
    public Sprite chilliPepperSprite;
    public Sprite cornSprite;
    public Sprite eggplantSprite;
    public Sprite garlicSprite;
    public Sprite mushroomSprite;
    public Sprite pepperSprite;
    public Sprite pumpkinSprite;
    public Sprite turnipSprite;
}

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

}

using System;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        rollingPin,
        cookingPot,
        ladle,
        cookingKnife,
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite() {
        switch (itemType) {
        default:
        case ItemType.rollingPin:   return ItemAssets.Instance.rollingPinSprite;
        case ItemType.cookingPot:   return ItemAssets.Instance.cookingPotSprite;
        case ItemType.ladle:        return ItemAssets.Instance.ladleSprite;
        case ItemType.cookingKnife: return ItemAssets.Instance.cookingKnifeSprite;
        }
    }


    public bool IsStackable() {
        switch (itemType) {
        default:
            return true;
            case ItemType.rollingPin:
            case ItemType.cookingPot:
            case ItemType.ladle:
            case ItemType.cookingKnife:
                return false;
        }
    }

}

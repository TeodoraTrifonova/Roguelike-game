using System;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        rollingPin,
        cookingPot,
        ladle,
        cookingKnife,
        redMeat,
        chicken,
        pork,
        turkey,
        bacon,
        apple,
        banana,
        grape,
        lemon,
        lime,
        orange,
        pear,
        strawberry,
        carrot,
        chilliPepper,
        corn,
        eggplant,
        garlic,
        mushroom,
        pepper,
        pumpkin,
        turnip
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
        case ItemType.redMeat:      return ItemAssets.Instance.redMeatSprite;
        case ItemType.chicken:      return ItemAssets.Instance.chickenSprite;
        case ItemType.pork:         return ItemAssets.Instance.porkSprite;
        case ItemType.turkey:       return ItemAssets.Instance.turkeySprite;
        case ItemType.bacon:        return ItemAssets.Instance.baconSprite;
        case ItemType.apple:        return ItemAssets.Instance.appleSprite;
        case ItemType.banana:       return ItemAssets.Instance.bananaSprite;
        case ItemType.grape:        return ItemAssets.Instance.grapeSprite;
        case ItemType.lemon:        return ItemAssets.Instance.lemonSprite;
        case ItemType.lime:         return ItemAssets.Instance.limeSprite;
        case ItemType.orange:       return ItemAssets.Instance.orangeSprite;
        case ItemType.pear:         return ItemAssets.Instance.pearSprite;
        case ItemType.strawberry:   return ItemAssets.Instance.strawberrySprite;
        case ItemType.carrot:       return ItemAssets.Instance.carrotSprite;
        case ItemType.chilliPepper: return ItemAssets.Instance.chilliPepperSprite;
        case ItemType.corn:         return ItemAssets.Instance.cornSprite;
        case ItemType.eggplant:     return ItemAssets.Instance.eggplantSprite;
        case ItemType.garlic:       return ItemAssets.Instance.garlicSprite;
        case ItemType.mushroom:     return ItemAssets.Instance.mushroomSprite;
        case ItemType.pepper:       return ItemAssets.Instance.pepperSprite;
        case ItemType.pumpkin:      return ItemAssets.Instance.pumpkinSprite;
        case ItemType.turnip:       return ItemAssets.Instance.turnipSprite;
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

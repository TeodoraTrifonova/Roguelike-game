using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour {

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        transform.GetComponent<Collider2D>().enabled = false;
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item) {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 2.5f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 12f, ForceMode2D.Impulse);
        itemWorld.GetComponent<Collider2D>().enabled = true;
        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer = new SpriteRenderer();
    private TextMeshPro textMeshPro;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.GetComponentInChildren<TextMeshPro>();
        SetItem(new Item { itemType = GetItemBySprite(gameObject.GetComponent<SpriteRenderer>().sprite) , amount = 1 });
        if(!item.IsStackable())
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }


    public Item.ItemType GetItemBySprite(Sprite sprite)
    {
        if (sprite == ItemAssets.Instance.rollingPinSprite)
        {
            return Item.ItemType.rollingPin;
        }
        else if (sprite == ItemAssets.Instance.cookingPotSprite)
        {
            return Item.ItemType.cookingPot;
        }
        else if (sprite == ItemAssets.Instance.ladleSprite)
        {
            return Item.ItemType.ladle;
        }
        else if (sprite == ItemAssets.Instance.cookingKnifeSprite)
        {
            return Item.ItemType.cookingKnife;
        }
        else if (sprite == ItemAssets.Instance.redMeatSprite)
        {
            return Item.ItemType.redMeat;
        }
        else if (sprite == ItemAssets.Instance.chickenSprite)
        {
            return Item.ItemType.chicken;
        }
        else if (sprite == ItemAssets.Instance.porkSprite)
        {
            return Item.ItemType.pork;
        }
        else if (sprite == ItemAssets.Instance.turkeySprite)
        {
            return Item.ItemType.turkey;
        }
        else if (sprite == ItemAssets.Instance.baconSprite)
        {
            return Item.ItemType.bacon;
        }
        else if (sprite == ItemAssets.Instance.appleSprite)
        {
            return Item.ItemType.apple;
        }
        else if (sprite == ItemAssets.Instance.bananaSprite)
        {
            return Item.ItemType.banana;
        }
        else if (sprite == ItemAssets.Instance.grapeSprite)
        {
            return Item.ItemType.grape;
        }
        else if (sprite == ItemAssets.Instance.lemonSprite)
        {
            return Item.ItemType.lemon;
        }
        else if (sprite == ItemAssets.Instance.limeSprite)
        {
            return Item.ItemType.lime;
        }
        else if (sprite == ItemAssets.Instance.orangeSprite)
        {
            return Item.ItemType.orange;
        }
        else if (sprite == ItemAssets.Instance.pearSprite)
        {
            return Item.ItemType.pear;
        }
        else if (sprite == ItemAssets.Instance.strawberrySprite)
        {
            return Item.ItemType.strawberry;
        }
        else if (sprite == ItemAssets.Instance.carrotSprite)
        {
            return Item.ItemType.carrot;
        }
        else if (sprite == ItemAssets.Instance.chilliPepperSprite)
        {
            return Item.ItemType.chilliPepper;
        }
        else if (sprite == ItemAssets.Instance.cornSprite)
        {
            return Item.ItemType.corn;
        }
        else if (sprite == ItemAssets.Instance.eggplantSprite)
        {
            return Item.ItemType.eggplant;
        }
        else if (sprite == ItemAssets.Instance.garlicSprite)
        {
            return Item.ItemType.garlic;
        }
        else if (sprite == ItemAssets.Instance.mushroomSprite)
        {
            return Item.ItemType.mushroom;
        }
        else if (sprite == ItemAssets.Instance.pepperSprite)
        {
            return Item.ItemType.pepper;
        }
        else if (sprite == ItemAssets.Instance.pumpkinSprite)
        {
            return Item.ItemType.pumpkin;
        }
        else if (sprite == ItemAssets.Instance.turnipSprite)
        {
            return Item.ItemType.turnip;
        }
        else
        {
            Debug.LogWarning("Sprite for this not added, so I've chosen tochilka instead");
            return Item.ItemType.rollingPin;
        }
    }


    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        } else {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}

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
        else
        {
            Debug.LogWarning("Sprite ERROR");
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

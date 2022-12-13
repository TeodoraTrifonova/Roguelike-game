using UnityEngine;

public class Player : MonoBehaviour {
    
    public static Player Instance { get; private set; }
    
    [SerializeField] private UI_Inventory uiInventory;

    private Inventory inventory = new Inventory();

    private void Awake() {
        Instance = this;

        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}

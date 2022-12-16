using System;
using System.Collections.Generic;

public class Inventory {

    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory() {
        itemList = new List<Item>();

        itemList.Add(new Item { itemType = Item.ItemType.rollingPin, amount = 1 });
        itemList.Add(new Item { itemType = Item.ItemType.cookingPot, amount = 1 });
        itemList.Add(new Item { itemType = Item.ItemType.ladle, amount = 1 });
        itemList.Add(new Item { itemType = Item.ItemType.cookingKnife, amount = 1 });

       /* AddItem(new Item { itemType = Item.ItemType.rollingPin, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.cookingPot, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.ladle, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.cookingKnife, amount = 1 });*/
    }

    public void AddItem(Item item) 
    {
        bool partOfRecipe = false;

        foreach (var ingredient in RecipeManager.Ingredients)
        {
            var ingredientItemType = ingredient.GetComponent<ItemWorld>().GetItem().itemType;
            if (ingredientItemType == item.itemType             ||
                item.itemType == Item.ItemType.rollingPin  ||
                item.itemType == Item.ItemType.ladle       ||
                item.itemType == Item.ItemType.cookingPot  ||
                item.itemType == Item.ItemType.cookingKnife||
                item.itemType == Item.ItemType.grape)
            {
                partOfRecipe = true;
            }
        }
        
        if(partOfRecipe)
        {
            if (item.IsStackable())
            {
                bool itemAlreadyInInventory = false;
                foreach (Item inventoryItem in itemList)
                {
                    if (inventoryItem.itemType == item.itemType)
                    {
                        inventoryItem.amount += item.amount;
                        itemAlreadyInInventory = true;
                    }
                }
                if (!itemAlreadyInInventory)
                {
                    itemList.Add(item);
                }
            }
            else
            {
                itemList.Add(item);
            }
        }
        else
        {
            Backpack.AddItem();
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item) 
    {
        if (item.IsStackable()) 
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList) 
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0) 
            {
                itemList.Remove(itemInInventory);
            }
        } 
        else 
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList() {
        return itemList;
    }

}

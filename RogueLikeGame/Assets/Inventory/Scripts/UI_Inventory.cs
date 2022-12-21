using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using System;
using System.Collections.Generic;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    private Player player;
    private Color bgcolor;

    private void Awake()
    {
        bgcolor = itemSlotTemplate.Find("background").GetComponent<Image>().color;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
        inventory.CheckForAllIngredientsCollected();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;
        int newWeapon = -1;

        foreach (Item item in inventory.GetItemList())
        {

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);

            if(newWeapon == -1)
            {
                if (item.itemType == Item.ItemType.rollingPin)
                {
                    newWeapon = 0;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.cookingPot)
                {
                    newWeapon = 1;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.ladle)
                {
                    newWeapon = 2;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.cookingKnife)
                {
                    newWeapon = 3;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                player.gameObject.GetComponentInChildren<PlayerShooting>().selectedWeapon = newWeapon;
            }

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                ChangeAllBackgrounds();
                
                if (item.itemType == Item.ItemType.rollingPin)
                {
                    newWeapon = 0;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.cookingPot)
                {
                    newWeapon = 1;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.ladle)
                {
                    newWeapon = 2;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                else if (item.itemType == Item.ItemType.cookingKnife)
                {
                    newWeapon = 3;
                    itemSlotRectTransform.Find("background").GetComponent<Image>().color = new Color(97, 63, 49, 0.5f);
                }
                player.gameObject.GetComponentInChildren<PlayerShooting>().selectedWeapon = newWeapon;

            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }

            else
            {
                uiText.SetText("");
            }

            x++;
            if (x >= 4)
            {
                x = 0;
                y++;
            }
        }
    }

    private void ChangeAllBackgrounds()
    {
        foreach (Transform child in itemSlotContainer)
        {
            child.Find("background").GetComponent<Image>().color = bgcolor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Backpack 
{
    private static int itemsNotRecipeCount = 0;

    public static int ItemsCount { get => itemsNotRecipeCount; }

    public static void AddItem()
    {
        itemsNotRecipeCount++;
    }

    public static void RemoveItem()
    {
        itemsNotRecipeCount--;
    }

    public static void RemoveItems(int itemsCount)
    {
        itemsNotRecipeCount -= itemsCount;
    }
}

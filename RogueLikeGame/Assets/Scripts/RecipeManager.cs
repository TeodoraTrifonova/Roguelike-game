using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecipeManager 
{
    private static List<GameObject> m_ingredients;

    public static List<GameObject> Ingredients { get => m_ingredients; }

    public static void SetRecipe(List<GameObject> ingredients)
    {
        m_ingredients = ingredients;
        foreach (var item in ingredients)
        {
            Debug.Log(item.name);
        }
    }
}

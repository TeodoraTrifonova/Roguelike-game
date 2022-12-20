using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> meat;

    [SerializeField]
    private List<GameObject> vegetables;

    [SerializeField]
    private List<GameObject> fruit;

    private List<GameObject> recipeIngridients;


    private void Awake()
    {
        recipeIngridients = new List<GameObject>();

        GameObject meatIngredient = meat[Random.Range(0, meat.Count)];
        meatIngredient.GetComponent<ItemWorld>().ItemSetup();
        recipeIngridients.Add(meatIngredient);

        GameObject vegetableIngredient = vegetables[Random.Range(0, vegetables.Count)];
        vegetableIngredient.GetComponent<ItemWorld>().ItemSetup();
        recipeIngridients.Add(vegetableIngredient);

        GameObject fruitIngredient = fruit[Random.Range(0, fruit.Count)];
        fruitIngredient.GetComponent<ItemWorld>().ItemSetup();
        recipeIngridients.Add(fruitIngredient);


        RecipeManager.SetRecipe(recipeIngridients);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public static Crafting Instance;

    public List<Recipe> Recipes = new();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Recipe[] recipe = Resources.LoadAll<Recipe>("Assets/Recipes");
        if(recipe != null)
        {
            foreach(Recipe foundRecipe in recipe)
            {
                Debug.Log(foundRecipe);
                Recipes.Add(foundRecipe);
            }
        }
        else
        {
            Debug.Log("NO RECIPES WERE FOUND");
        }
        
    }
    public bool CheckWorkstation(Item item, bool mortar, bool furnace, bool cuttingBoard, bool cauldron)
    {
        if(mortar)
        {
            foreach(Recipe recipe in Recipes)
            {
                if (recipe.mortar != mortar) { continue; }
                if(recipe.firstItem != item && recipe.secondItem != item && recipe.thirdItem != item) { continue; }
                else { return true; }
            }
        }
        if (furnace)
        {
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.furnace != furnace) { continue; }
                if (recipe.firstItem != item && recipe.secondItem != item && recipe.thirdItem != item) { continue; }
                else { return true; }
            }
        }
        if (cuttingBoard)
        {
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.cuttingBoard != cuttingBoard) { continue; }
                if (recipe.firstItem != item && recipe.secondItem != item && recipe.thirdItem != item) { continue; }
                else { return true; }
            }
        }
        if (cauldron)
        {
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.cauldron != cauldron) { continue; }
                if (recipe.firstItem != item && recipe.secondItem != item && recipe.thirdItem != item) { continue; }
                else { return true; }
            }
        }
        return false;
    }
    public Item Craft(Item item1, int item1Amount, Item item2, int item2Amount, Item item3, int item3Amount,
        bool mortar, bool furnace, bool cuttingBoard, bool cauldron)
    {
        List<Item> items = new List<Item>();
        Dictionary<Item, int> itemAmount = new Dictionary<Item, int>();

        if(item1 != null)
        {
            items.Add(item1);
            itemAmount.Add(item1, item1Amount);
        }
        if(item2 != null)
        {
            items.Add(item2);
            itemAmount.Add(item2, item2Amount);
        }
        if(item3 != null)
        {
            items.Add(item3);
            itemAmount.Add(item3, item3Amount);
        }
        foreach (Recipe recipe in Recipes)
        {
            if(recipe.mortar != mortar) { continue; }
            if(recipe.furnace != furnace) { continue; }
            if(recipe.cuttingBoard != cuttingBoard) { continue; }
            if(recipe.cauldron != cauldron) { continue; }

            bool first = false;
            bool second = false;
            bool third = false;

            foreach(Item item in items)
            {
                if(recipe.firstItem != null)
                {
                    if(recipe.firstItem == item)
                    {
                        if(recipe.firstItemAmount == itemAmount[item])
                        {
                            first = true;
                        }
                    }
                }
                else
                {
                    first = true;
                }
                if(recipe.secondItem != null)
                {
                    if (recipe.secondItem == item)
                    {
                        if (recipe.secondItemAmount == itemAmount[item])
                        {
                            second = true;
                        }
                    }
                }
                else if(items.Count < 2)
                {
                    second = true;
                }
                if (recipe.thirdItem != null)
                {
                    if (recipe.thirdItem == item)
                    {
                        if (recipe.thirdItemAmount == itemAmount[item])
                        {
                            third = true;
                        }
                    }
                }
                else if(items.Count < 3)
                {
                    third = true;
                }
            }
            if (first && second && third)
            {
                Debug.Log(recipe);
                return recipe.result;
            }
        }
        return null;
    }
}

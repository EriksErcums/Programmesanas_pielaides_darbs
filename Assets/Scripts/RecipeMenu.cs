using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeMenu : MonoBehaviour
{
    public static RecipeMenu Instance;
    [HideInInspector]
    public bool isOpen = false;
    public Animator animator;

    public GameObject mortarRecipes;
    public GameObject furnaceRecipes;
    public GameObject cauldronRecipes;

    private List<GameObject> RecipesList = new List<GameObject>();

    public Button furnace;
    public Button cauldron;
    public Button mortar;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RecipesList.Add(mortarRecipes);
        RecipesList.Add(furnaceRecipes);
        RecipesList.Add(cauldronRecipes);
    }

    public void OpenCloseMenu()
    {
        if (isOpen)
        {
            isOpen = false;
            animator.SetTrigger("isOpen");
        }
        else
        {
            isOpen = true;
            animator.SetTrigger("isOpen");
        }
    }
    private void MenuController(GameObject menu)
    {
        foreach(GameObject recipeMenu in RecipesList)
        {
            if(recipeMenu != menu)
                recipeMenu.SetActive(false);
            else
                recipeMenu.SetActive(true);
        }
    }
    public void FurnaceRecipe()
    {
        MenuController(furnaceRecipes);
    }
    public void MortarRecipe()
    {
        MenuController(mortarRecipes);
    }
    public void CauldronRecipe()
    {
        MenuController(cauldronRecipes);
    }
}

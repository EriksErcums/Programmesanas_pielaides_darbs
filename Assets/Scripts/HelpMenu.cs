using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpMenu : MonoBehaviour
{
    public static HelpMenu Instance;

    [HideInInspector]
    public bool isOpenMenu = false;
    public Animator animator;

    public RecipeMenu Recipe;

    public TextMeshProUGUI text;

    [TextArea(3, 10)]
    public string knifeHelp;
    [TextArea(3, 10)]
    public string mortalHelp;
    [TextArea(3, 10)]
    public string furnaceHelp;
    [TextArea(3, 10)]
    public string cauldronHelp;
    [TextArea(3, 10)]
    public string sleepHelp;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenCloseMenu()
    {
        if (!GameObject.Find("Bedroom"))
        {
            var activeMenus = GameObject.Find("InteractiveMenus").GetComponent<MenuManager>().activeMenus;

            foreach(var menu in activeMenus)
            {
                switch (menu.name)
                {
                    case "Cutting board Menu":
                        text.text = knifeHelp;
                        break;
                    case "Furnace Menu":
                        text.text = furnaceHelp;
                        break;
                    case "CauldronMenu":
                        text.text = cauldronHelp;
                        break;
                    case "MortalAndPestleMenu":
                        text.text = mortalHelp;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            text.text = sleepHelp;
        }
        if (!isOpenMenu)
        {
            if (Recipe.isOpen)
            {
                Recipe.OpenCloseMenu();
            }
            animator.SetTrigger("isOpen");
            isOpenMenu = true;
        }
        else
        {
            animator.SetTrigger("isOpen");
            isOpenMenu = false;
        }
    }
}

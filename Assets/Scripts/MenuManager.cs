using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public List<GameObject> activeMenus = new List<GameObject>();
    public List<GameObject> disabledMenus = new List<GameObject>();

    public GameObject pauseMenu;

    public bool activeCookGame = false;

    private SoundEffects soundEffects;

    private void Awake()
    {
        Instance = this;
        soundEffects = SoundEffects.Instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeMenus.Contains(GameObject.Find("Cutting board Menu"))) { CloseCuttingBoardMenu();  }
            else if (activeMenus.Contains(GameObject.Find("Furnace Menu"))) { CloseFurnaceMenu();  }
            else if (activeMenus.Contains(GameObject.Find("MortalAndPestleMenu"))) { CloseMortalAndPestleMenu();  }
            else if (activeMenus.Contains(GameObject.Find("CauldronMenu"))) { CloseCauldrenMenu();  }
            else { pauseMenu.SetActive(true); Time.timeScale = 0f; }
        }       
    }

    private void CloseCuttingBoardMenu()
    {
        /*if (GameObject.FindGameObjectWithTag("FoodObj"))
        {
            soundEffects.WrongSound();
        }
        else
        {
            UseKnife knife = GameObject.Find("Knife").GetComponent<UseKnife>();
            if (!(knife.knifeActive))
            {
                knife.DisableKnife();              
            }
            TurnOff();
        }*/
    }
    private void CloseFurnaceMenu()
    {
        /*if (!activeCookGame)
        {
            GameObject[] placedOb = GameObject.FindGameObjectsWithTag("FoodObj");
            foreach (GameObject obj in placedOb)
            {
                obj.GetComponent<FurnaceIneventoryItemController>().RemoveItem();
            }
            TurnOff();
            GameObject.Find("Environment/Furnace").GetComponent<OpenFurnace>().ClsoeDoor();
        }*/
    }
    private void CloseMortalAndPestleMenu()
    {
        /*if (GameObject.Find("InteractiveMenus/MortalAndPestleMenu/Pestle"))
        {
            Pestle pestle = GameObject.Find("InteractiveMenus/MortalAndPestleMenu/Pestle").GetComponent<Pestle>();
            if (pestle.activePestle)
            {
                pestle.DeactivatePestle();
            }
        }
        TurnOff();*/
    }

    private void CloseCauldrenMenu()
    {
        if (GameObject.Find("InteractiveMenus/CauldronMenu/Spoon"))
        {
            Spon spon = GameObject.Find("InteractiveMenus/CauldronMenu/Spoon").GetComponent<Spon>();
            if (spon.activeSpon)
            {
                spon.DeactivateSpon();
            }
        }
        TurnOff();
    }
    
    private void TurnOff()
    {
        foreach (var obj in activeMenus)
        {
            if (obj.tag == "UI")
            {
                StartCoroutine(resetAnimation(obj));
            }
            else
            {
                obj.SetActive(false);
            };
        }
        activeMenus.Clear();
        foreach(var obj in disabledMenus)
        {
            obj.SetActive(true);
        }
        disabledMenus.Clear();
    }

    private IEnumerator resetAnimation(GameObject obj)
    {
        if (obj.GetComponent<HelpMenu>())
        {
            HelpMenu help = obj.GetComponent<HelpMenu>();
            if (help.isOpenMenu)
            {
                help.OpenCloseMenu();
                Animator animator = help.GetComponent<Animator>();
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 2);
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(false);
            }
        }
        else
        {
            RecipeMenu recipe = obj.GetComponent<RecipeMenu>();
            if (recipe.isOpen)
            {
                recipe.OpenCloseMenu();
                Animator animator = recipe.GetComponent<Animator>();
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 2);
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}

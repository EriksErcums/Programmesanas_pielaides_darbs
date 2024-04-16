using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour
{
    public static ClickObject Instance;
    public List<GameObject> menuObjectsActive;
    public List<GameObject> menuObjectsDisabled;

    public bool canOpen = true;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenus()
    {
        if (canOpen)
        {
            GameObject menuManager = GameObject.Find("InteractiveMenus");
            foreach (var obj in menuObjectsActive)
            {
                obj.SetActive(true);
                menuManager.GetComponent<MenuManager>().activeMenus.Add(obj);
            }

            foreach (var obj in menuObjectsDisabled)
            {
                obj.SetActive(false);
                menuManager.GetComponent<MenuManager>().disabledMenus.Add(obj);
            }
        }
    }
}

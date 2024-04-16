using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScenes : MonoBehaviour
{
    public static SwapScenes Instance;
    public GameObject Kitchen;
    public GameObject ShopFront;
    public GameObject Bedroom;

    public List<GameObject> KitchenUI = new List<GameObject>();
    public List<GameObject> ShopFrontUI = new List<GameObject>();
    public List<GameObject> BedroomUI = new List<GameObject>();

    private List<GameObject> sceneList = new List<GameObject>();
    private List<List<GameObject>> UIObjects = new List<List<GameObject>>();

    private EventManager eventManager;

    private void Awake()
    {
        Instance = this;

        eventManager = EventManager.Instance;
        sceneList.Add(Kitchen);
        sceneList.Add(ShopFront);
        sceneList.Add(Bedroom);

        UIObjects.Add(KitchenUI);
        UIObjects.Add(ShopFrontUI);
        UIObjects.Add(BedroomUI);
    }

    private void Swap(GameObject scene, List<GameObject> list)
    {
        //scene
        foreach(GameObject sceneObj in sceneList)
        {
            if(sceneObj != scene)
            { 
                sceneObj.SetActive(false);
            }
            else
            {
                sceneObj.SetActive(true);
            }
        }
        //ui lists
        foreach(List<GameObject> UILists in UIObjects)
        {
            if(UILists != list)
            {
                foreach(GameObject UIobj in UILists)
                {
                    if(!list.Contains(UIobj))
                        UIobj.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject UIobj in UILists)
                {
                    UIobj.SetActive(true);
                }
            }
        }
    }

    public void ToShopFront()
    {
        Swap(ShopFront, ShopFrontUI);
    }

    public void ToKitchen()
    {
        Swap(Kitchen, KitchenUI);
    }

    public void ToBedroom()
    {
        Swap(Bedroom, BedroomUI);
    }
}

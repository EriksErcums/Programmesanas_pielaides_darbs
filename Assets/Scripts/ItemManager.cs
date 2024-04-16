using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public List<Item> MerchantItems = new List<Item>();
    public List<Item> AllItems = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public Item FindItem(string itemName)
    {
        foreach(Item item in AllItems)
        {
            if(item.itemName == itemName) return item;
        }
        return null;
    }
    public Item FindItemByIndex(string itemIndex)
    {
        foreach (Item item in AllItems)
        {
            if (item.itemIndex == itemIndex) return item;
        }
        return null;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [HideInInspector]
    public List<Item> Items = new List<Item> ();
    [HideInInspector]
    public Dictionary<Item, int> ItemAmount = new Dictionary<Item, int> ();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Sprite bronzeStar;
    public Sprite silverStar;
    public Sprite goldStar;

    public AddItem[] addItems;

    private void Awake()
    {
        Instance = this;
        
        foreach(AddItem item in addItems)
        {
            for(int i = 0; i < item.Amount; i++)
            {
                Add(item.Item);
            }
        }
        ListItems();
    }

    public void Add(Item item)
    {
        if (Items.Contains(item))
        {
            ItemAmount[item]++;
        }
        else
        {
            Items.Add(item);
            ItemAmount.Add(item, 1);
        }
        ListItems();
    }
    
    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            if (ItemAmount[item] > 1)
            {
                ItemAmount[item]--;
            }
            else
            {
                Items.Remove(item);
                ItemAmount.Remove(item);
            }
        }
        ListItems();
    }
    private void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemAmount = obj.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>();
            var itemQuality = obj.transform.Find("ItemQuality").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
            itemAmount.text = ItemAmount[item].ToString();

            switch (item.itemQuality)
            {
                case 0:
                    itemQuality.enabled = false;
                    break;
                case 1:
                    itemQuality.sprite = bronzeStar;
                    break;
                case 2:
                    itemQuality.sprite = silverStar;
                    break;
                case 3:
                    itemQuality.sprite = goldStar;
                    break;
                default:
                    Debug.Log("QUALITY SYSTEM FAILED TO CHOSE THE CORRECT SPRITE FOR QUALITY");
                    break;
            }
        }
    }
    public Item GetItem(string itemName)
    {
        foreach(var item in Items)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        Debug.Log("ITEM NOT FOUND");
        return null;
    }
}

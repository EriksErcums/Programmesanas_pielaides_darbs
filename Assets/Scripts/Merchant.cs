using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Merchant : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;

    public List<Item> sellingItems = new List<Item>();
    public Dictionary<Item, int> sellingItemAmount = new Dictionary<Item, int>();

    public Sprite bronzeStar;
    public Sprite silverStar;
    public Sprite goldStar;

    private EnvironmentManager enManager;
    public void Start()
    {
        SetItems();
        enManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        if (!enManager)
        {
            Debug.Log("NPC cant find EnvironmentManager");
        }
        else
        {
            enManager.AddNPC(gameObject);
        }
    }
    private void OnDestroy()
    {
        enManager.RemoveNPC();
        EventManager.Instance.setSpawn(true);
    }
    private void Update()
    {
        if (!GameObject.Find("ShopFront"))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    public void DeleteMerchant()
    {
        Destroy(gameObject);
    }
    public void BuyItem(Item item)
    {
        var moneyManager = MoneyManager.Instance;
        if(moneyManager.Money >= item.itemPrice)
        {
            moneyManager.RemoveMoney(item.itemPrice);
            InventoryManager.Instance.Add(item);
            Remove(item);
        }
    }
    public void Remove(Item item)
    {
        if (sellingItems.Contains(item))
        {
            if(sellingItemAmount[item] > 1)
            {
                sellingItemAmount[item]--;
            }
            else
            {
                sellingItems.Remove(item);
                sellingItemAmount.Remove(item);
            }
        }
        ListItems();
    }
    private void SetItems()
    {
        int amount = Random.Range(1, 4);
        var MerchantItems = ItemManager.Instance.MerchantItems;
        for (int i = 0; i < amount; i++)
        {
            Item item = MerchantItems[Random.Range(0, MerchantItems.Count)];
            if (sellingItems.Contains(item))
            {
                bool contains = true;
                while (contains)
                {
                    item = MerchantItems[Random.Range(0, MerchantItems.Count)];
                    if (!sellingItems.Contains(item))
                    {
                        contains = false;
                    }
                }
            }
            sellingItems.Add(item);
            sellingItemAmount.Add(sellingItems[i], Random.Range(1, 10));
        }
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in sellingItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemAmount = obj.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>();
            var itemQuality = obj.transform.Find("ItemQuality").GetComponent<Image>();
            var itemPrice = obj.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
            itemAmount.text = sellingItemAmount[item].ToString();
            itemPrice.text = item.itemPrice.ToString();
            

            itemQuality.enabled = false;

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
        foreach (var item in sellingItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }
}

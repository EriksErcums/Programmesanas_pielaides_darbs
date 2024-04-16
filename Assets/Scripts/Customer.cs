using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Customer : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    [HideInInspector]
    public List<Item> orderedItems = new List<Item>();
    [HideInInspector]
    public Dictionary<Item, int> orderedItemsAmount = new Dictionary<Item, int>();
    private int price;

    public TextMeshProUGUI text;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Sprite bronzeStar;
    public Sprite silverStar;
    public Sprite goldStar;

    public GameObject dialogueTextBox;
    public GameObject shopTextBox;

    private EnvironmentManager enManager;

    private void Start()
    {
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
    public void Accept()
    {
        var inventory = InventoryManager.Instance;
        var itemManager = ItemManager.Instance;
        bool hasAllItems = true;
        bool itemFound = false;
        price = 0;
        foreach(Item item in orderedItems)
        {
            foreach (Item itemInv in inventory.Items)
            {
                if(item.itemIndex == itemInv.itemIndex)
                {
                    foreach (Item itemInItemManager in itemManager.AllItems)
                    {
                        itemFound = true;
                        Debug.Log($"Quality: {itemInv.itemQuality}, Price: {itemInv.itemPrice}, Name: {itemInv.name}");
                        price += itemInv.itemPrice;
                        break;
                    }
                }
            }
            if (!itemFound)
            {
                hasAllItems = false;
                break;
            }
            else
            {
                itemFound = false;
            }
        }
        if (hasAllItems)
        {
            List<Item> placeHolder = new List<Item>();
            foreach(Item item in orderedItems)
            {
                foreach(Item itemInv in inventory.Items)
                {
                    if(itemInv.itemIndex == item.itemIndex)
                    {
                        for (int i = 0; i < orderedItemsAmount[item]; i++)
                        {
                            placeHolder.Add(itemInv);
                        }
                    }
                }
            }
            foreach(Item item in placeHolder)
            {
                inventory.Remove(item);
            }
            MoneyManager.Instance.AddMoney(price);
            ListItems();
            shopTextBox.SetActive(false);
            gameObject.GetComponent<DialogueManager>().canIncrement = true;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("MISSING ITEMS IN INVENTORY");
        }
    }

    public void Decline()
    {
        ListItems();
        shopTextBox.SetActive(false);
        Destroy(gameObject);
    }

    public void SetRequest()
    {
        var amountOfItem = Random.Range(1, 2);
        string textPhrase = "Hello there! Do you have these?";
        for (int i = 0; i <= amountOfItem; i++)
        {
            Item x = items[Random.Range(0, items.Count)];
            if (orderedItems.Contains(x))
            {
                orderedItemsAmount[x]++;
            }
            else
            {
                orderedItems.Add(x);
                orderedItemsAmount.Add(x, 1);
            }
        }
        text.text = textPhrase;
        ListItems();
    }

    public IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(2f); 
        Destroy(gameObject);
    }
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in orderedItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemAmount = obj.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>();
            var itemQuality = obj.transform.Find("ItemQuality").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
            itemAmount.text = orderedItemsAmount[item].ToString();
            itemQuality.enabled = false;
        }
    }
}

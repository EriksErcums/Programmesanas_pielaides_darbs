using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemController : MonoBehaviour
{
    [HideInInspector]
    public Item item;
    public void RemoveItem()
    {
        item = InventoryManager.Instance.GetItem(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        Instantiate(item.itemPrefab);
        InventoryManager.Instance.Remove(item);      
    }

    public void BuyItem()
    {
        var merchant = GameObject.Find("Merchant(Clone)");
        item = merchant.GetComponent<Merchant>().GetItem(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        merchant.GetComponent<Merchant>().BuyItem(item);

    }
}

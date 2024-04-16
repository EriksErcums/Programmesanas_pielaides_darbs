 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemPrice;
    public int itemQuality;
    public string itemIndex;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    //Quality 0-none 1-bronze 2-silver 3-gold
}

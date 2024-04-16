using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    //Money
    public int money;

    //Time
    public int day;
    public int week;

    //Inventory
    public List<string> items = new List<string>();

    public List<int> itemAmount = new List<int>();

    //NPC
    //Blacksmith
    public int blacksimthInteractions;
    //Merchant
    public int merchantInteractions;
    //Priest
    public int priestIntercations;
    //Jester
    public int jesterInteractions;
    //warnings
    public int warnings;
    //weekly income
    public double weeklyIncome;
    //tutorial
    public bool playTutorial;
    public Data()
    {
        money = MoneyManager.Instance.Money;

        day = GameObject.Find("TimeManager").GetComponent<TimeManager>().getDay();
        week = GameObject.Find("TimeManager").GetComponent<TimeManager>().getWeek();

        var inventory = InventoryManager.Instance;
        /*foreach (Item item in inventory.Items)
        {
            items.Add(item.itemName);
            itemAmount.Add(inventory.ItemAmount[item]);
        }*/

        warnings = GlobalManager.Instance.warnings;
        weeklyIncome = GlobalManager.Instance.weeklyIncome;

        playTutorial = GlobalManager.Instance.playTutorial;
    }
}

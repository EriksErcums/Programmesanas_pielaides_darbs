using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (FindObjectOfType<GlobalManager>())
        {
            if (FindObjectOfType<GlobalManager>().LoadGame)
            {
                LoadData();
            }
            Tutorial.Instance.TutorialStart();
        }
    }
    public void SaveData()
    {
        SaveSystem.SaveData();
        Debug.Log("Data saved");
    }

    public void LoadData()
    {
        Debug.Log("Loading save...");
        if(File.Exists(Application.persistentDataPath + "/saveData.data"))
        {
            Data data = SaveSystem.LoadData();

            //Money
            MoneyManager.Instance.SetMoney(data.money);

            //Time
            GameObject.Find("TimeManager").GetComponent<TimeManager>().setDay(data.day);
            GameObject.Find("TimeManager").GetComponent<TimeManager>().setWeek(data.week);

            //Inventory
            var inventory = InventoryManager.Instance;
            var itemManager = ItemManager.Instance;
            //inventory.Items.Clear();
            //inventory.ItemAmount.Clear();
            int i = 0;
            foreach (string item in data.items)
            {
                Item foundItem = itemManager.FindItem(item);
                if (foundItem != null)
                {
                    //inventory.Items.Add(foundItem);
                    //inventory.ItemAmount.Add(foundItem, data.itemAmount[i]);
                    i++;
                }
            }

            //Warnings
            GlobalManager.Instance.warnings = data.warnings;
            //WeeklyIncome
            GlobalManager.Instance.weeklyIncome = data.weeklyIncome;
            //Tutorial
            GlobalManager.Instance.playTutorial = data.playTutorial;
            Debug.Log("Loaded save");
        }
    }
}

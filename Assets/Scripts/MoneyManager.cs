using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int Money { get; set; }

    public TextMeshProUGUI moneyDisplay;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Money = 10;
        UpdateMoneyDisplay();
    }
    public void SetMoney(int amount)
    {
        Money = amount;
        UpdateMoneyDisplay();
    }
    public void AddMoney(int amount)
    {
        Money += amount;
        if (!GlobalManager.Instance)
        {
            Debug.Log("GlobalManager was not found!");
        }
        else
        {
            GlobalManager.Instance.weeklyIncome += amount;
        }
        UpdateMoneyDisplay();
    }
    public void RemoveMoney(int amount)
    {
        Money -= amount;
        UpdateMoneyDisplay();
    }
    private void UpdateMoneyDisplay()
    {
        moneyDisplay.text = Money.ToString();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnDayChanged;
    public static Action OnWeekChanged;

    public static int Day { get; set; }
    public static int Week { get; set; }

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI weekText;

    public GameObject bed;
    public GameObject nightPanel;
    public GameObject ui;

    public GameObject help;

    private SoundEffects soundEffects;

    public int getDay() { return Day; }
    public int getWeek() { return Week; }

    public void setDay(int day)
    { 
        Day = day;
        UpdateUIText();
    }
    public void setWeek(int week) 
    { 
        Week = week;
        UpdateUIText();
    }



    private void Start()
    {
        soundEffects = SoundEffects.Instance;
        Day = 1;
        OnDayChanged?.Invoke();
        Week = 1;
        OnWeekChanged?.Invoke();
        UpdateUIText();
    }
    public void Night()
    {
        StartCoroutine(SleepNight());
    }

    public IEnumerator SleepNight()
    {
        bed.GetComponent<Button>().enabled = false;
        ui.SetActive(false);
        nightPanel.SetActive(true);

        soundEffects.SleepSound();
        Animator animator = nightPanel.GetComponentInChildren<Animator>();
        animator.SetTrigger("Sleep");
        yield return new WaitForSeconds(4.5f);

        PassDay();
        nightPanel.SetActive(false);
        ui.SetActive(true);
        bed.GetComponent<Button>().enabled = true;
    }
    public void PassDay()
    {
        Day++;
        OnDayChanged?.Invoke();
        if(Day >= 7)
        {
            Week++;
            Day = 1;
            OnWeekChanged?.Invoke();
        }
        UpdateUIText();

        if(GlobalManager.Instance != null)
        {
            if (GlobalManager.Instance.activeSaveSystem)
            {
                SaveSystemManager.Instance.SaveData();
            }
        }
    }

    private void UpdateUIText()
    {
        dayText.text = "Day:" + Day.ToString();
        weekText.text = "Week:" + Week.ToString();
    }
}

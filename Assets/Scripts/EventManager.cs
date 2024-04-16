using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public GameObject shopFront;
    //Customer
    public GameObject customer;
    public GameObject customerParent;
    //NPC
    public GameObject merchant;
    public GameObject blacksmith;
    public GameObject jester;
    public GameObject priest;
    public GameObject taxMan;

    [HideInInspector]
    public List<GameObject> activeNpcs = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> toSpawn = new List<GameObject>();
    private bool canSpawn = true;

    private bool viewChanged = false;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        TimeManager.OnDayChanged += DayChange;
        TimeManager.OnWeekChanged += DayChange;

        TimeManager.OnDayChanged += NPCSchedule;
    }

    private void OnDisable()
    {
        TimeManager.OnDayChanged -= DayChange;
        TimeManager.OnWeekChanged -= DayChange;

        TimeManager.OnDayChanged -= NPCSchedule;
    }
    public void setSpawn(bool value) { canSpawn = value; }

    private void DayChange()
    {
        Debug.Log($"Day: {TimeManager.Day:00} Week: {TimeManager.Week:00}");
    }
    private void Update()
    {
        if (shopFront.activeSelf && canSpawn && toSpawn.Count > 0)
        {
            StartCoroutine(Spawn());
        }
    }
    private IEnumerator Spawn()
    {
        setSpawn(false);
        yield return new WaitForSeconds(1f);
        activeNpcs.Add(Instantiate(toSpawn[0], customerParent.transform));
        toSpawn.RemoveAt(0);
    }
    private void NPCSchedule()
    {
        setSpawn(false);
        foreach (GameObject npc in activeNpcs)
        {
            if(npc != null)
            {                
                Destroy(npc);
            }
        }
        activeNpcs.Clear();
        toSpawn.Clear();
        setSpawn(true);
        switch (TimeManager.Day)
        {
            case 1:
                toSpawn.Add(blacksmith);
                toSpawn.Add(merchant);
                toSpawn.Add(jester);
                break;
            case 2:
                toSpawn.Add(priest);
                toSpawn.Add(blacksmith);
                break;
            case 3:
                toSpawn.Add(jester);
                toSpawn.Add(merchant);
                break;
            case 4:
                toSpawn.Add(priest);
                toSpawn.Add(jester);
                break;
            case 5:
                toSpawn.Add(jester);
                toSpawn.Add(blacksmith);
                toSpawn.Add(merchant);
                break;
            case 6:
                toSpawn.Add(priest);
                toSpawn.Add(blacksmith);
                break;
            case 7:
                toSpawn.Add(jester);
                toSpawn.Add(merchant);
                break;
            default:
                Debug.Log("DAY OUT OF RANGE");                                              
                break;
        }
    }
}

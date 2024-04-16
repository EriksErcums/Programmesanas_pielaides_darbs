using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cauldron : MonoBehaviour
{
    private bool activeCauldron = false;
    private List<Item> inventory = new List<Item>();
    private Dictionary<Item, int> inventoryItemAmount = new Dictionary<Item, int>();

    public TextMeshProUGUI cauldronInventoryList;
    public bool GetCauldronState() { return activeCauldron; }
    //Check if inventory has items and return false if inventory is empty 
    public bool CheckItemInventory() { return inventory.Count > 0; }

    //Brewing
    private bool left = false;
    private bool right = false;
    private int fullRotations = 0;

    //Sounds
    private SoundEffects soundEffects;

    //Crafting
    private Item result;

    //Fire and smoke
    public GameObject fire;
    public GameObject smoke;

    private void Start()
    {
        soundEffects = SoundEffects.Instance;
    }
    public void AddItem(Item item)
    {
        if (inventory.Contains(item))
        {
            inventoryItemAmount[item]++;
        }
        else
        {
            inventory.Add(item);
            inventoryItemAmount.Add(item, 1);
        }
        ListItems();
    }
    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            if (inventoryItemAmount[item] > 1)
            {
                inventoryItemAmount[item]--;
            }
            else
            {
                inventory.Remove(item);
                inventoryItemAmount.Remove(item);
            }
        }
        ListItems();
    }
    public void EmptyCauldron()
    {
        StopGame();
        inventory.Clear();
        inventoryItemAmount.Clear();
        ListItems();
    }
    public void ReportHit(string side)
    {
        switch (side)
        {
            case "Left":
                //Debug.Log("left");
                left = true;
                CheackRotation();
                break;
            case "Right":
                //Debug.Log("right");
                right = true;
                CheackRotation();
                break;
            default:
                Debug.Log("ERORR: CAULDRON SIDE IS UNDEFINED");
                break;
        }
    }
    private void StartBrewing()
    {
        var fuelSystem = FurnaceFuelSystem.Instance;
        if (fuelSystem.GetFuel())
        {
            //crafting
            var crafting = Crafting.Instance;

            switch (inventory.Count)
            {
                case 1:
                    result = crafting.Craft(
                        inventory[0], inventoryItemAmount[inventory[0]],
                        null, 0,
                        null, 0,
                        false,
                        false,
                        false,
                        true);
                    break;
                case 2:
                    result = crafting.Craft(
                        inventory[0], inventoryItemAmount[inventory[0]],
                        inventory[1], inventoryItemAmount[inventory[1]],
                        null, 0,
                        false,
                        false,
                        false,
                        true);
                    break;
                case 3:
                    result = crafting.Craft(
                        inventory[0], inventoryItemAmount[inventory[0]],
                        inventory[1], inventoryItemAmount[inventory[1]],
                        inventory[2], inventoryItemAmount[inventory[2]],
                        false,
                        false,
                        false,
                        true);
                    break;
                default:
                    soundEffects.WrongSound();
                    RestRotations();
                    result = null;
                    Debug.Log("ERROR: NO RECIPE FOUND");
                    break;
            }
            if(result != null)
            {
                activeCauldron = true;
                fuelSystem.RemoveFuel();
                //animation
                FireAnimation(true);
            }
        }
        else
        {
            soundEffects.WrongSound();
            RestRotations();
        }
    }
    private void StopGame()
    {
        RestRotations();
        if(result != null)
        {
            InventoryManager.Instance.Add(result);
        }
        result = null;
        inventory.Clear();
        inventoryItemAmount.Clear();
        ListItems();
        smoke.GetComponent<Animator>().Play("Smoke", 0, 0f);
        FireAnimation(false);
        activeCauldron = false;
    }
    private void CheackRotation()
    {
        if(activeCauldron)
        {
            if (left && right)
            {
                fullRotations++;
                left = false;
                right = false;
            }
            if(fullRotations >= 5)
            {
                StopGame();
            }
        }
        if(fullRotations <= 0 && !activeCauldron)
        {
            StartBrewing();
        }
    }
    private void RestRotations()
    {
        left = false;
        right = false;
        fullRotations = 0;
    }
    private void ListItems()
    {
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("FoodObj");
        foreach(GameObject foodObject in foodObjects)
        {
            if (foodObject != null)
            {
                Destroy(foodObject);
            }
        }
        string list = "";
        foreach(Item item in inventory)
        {
            list += item.name + " x" + inventoryItemAmount[item] + "\n";
        }
        cauldronInventoryList.text = list;
    }
    private void FireAnimation(bool state)
    {
        if (state)
        {
            fire.SetActive(state);
            fire.GetComponent<Animator>().SetBool("Fire", state);
        }
        else
        {
            StartCoroutine(FireTimer());
            fire.SetActive(state);
        }
    }
    private IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(2f);
        fire.GetComponent<Animator>().SetBool("Fire", false);
    }
}

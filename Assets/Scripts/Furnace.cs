using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Furnace : MonoBehaviour    
{
    public static Furnace Instance;

    //Door
    private bool isDoorOpen = false;

    public Animator animator;

    //Sound
    private SoundEffects soundEffects;

    //Furnace
    private bool activeFurnace = false;

    //Inventory
    private List<Item> inventory = new List<Item>();
    private Dictionary<Item, int> inventoryItemAmount = new Dictionary<Item, int>();

    public TextMeshProUGUI InventoryList;

    //Crafting
    private Item result;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        soundEffects = SoundEffects.Instance;
    }

    public bool GetFurnaceState() { return activeFurnace; }
    public void SetFurnaceState(bool state) {  activeFurnace = state; }
    public bool GetDoorState() { return isDoorOpen; }

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

    private void ListItems()
    {
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("FoodObj");
        foreach (GameObject foodObject in foodObjects)
        {
            if (foodObject != null)
            {
                Destroy(foodObject);
            }
        }
        string list = "";
        foreach (Item item in inventory)
        {
            list += item.name + " x" + inventoryItemAmount[item] + "\n";
        }
        InventoryList.text = list;
    }

    public void EmptyFurnace(bool returnItems)
    {
        if (!GetFurnaceState())
        {
            if (returnItems)
            {
                var inventoryManager = InventoryManager.Instance;
                foreach(Item item in inventory)
                {
                    inventoryManager.Add(item);
                }
            }
            inventory.Clear();
            inventoryItemAmount.Clear();
            ListItems();
        }
    }
    public void SetDoorState(bool doorState)
    {
        //open = true; closed = false
        if(doorState != isDoorOpen)
        {
            isDoorOpen = doorState;
            animator.SetBool("Door", doorState);
        }
        else
        {
            soundEffects.WrongSound();
        }
    }

    /// <Furnace idea>
    /// More you make of the same food the better chance to get better
    /// quality...
    /// 
    /// add same index to food with different quality and then after getting the food form crafting find the same foods as the index and chose the quality
    /// plus add the system that keeps track of the amount of time the said food has been made
    /// </summary>
    /// 

    private void Update()
    {
        var point = Camera.main.ScreenPointToRay(Input.mousePosition);
        var mouseOver = gameObject.GetComponentInChildren<PolygonCollider2D>().bounds.IntersectRay(point);
        if (GameObject.FindGameObjectWithTag("FoodObj") == null)
        {
            if (mouseOver && Input.GetMouseButtonDown(0))
            {
                if (!GetFurnaceState())
                {
                    if (GetDoorState())
                    {
                        if (inventory.Count == 0)
                        {
                            //If empty furnace and opened door close door
                            SetDoorState(false);
                        }
                        else
                        {
                            //If not empty furnace and opened door check for recipe
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
                                            true,
                                            false,
                                            false);
                                        break;
                                    case 2:
                                        result = crafting.Craft(
                                            inventory[0], inventoryItemAmount[inventory[0]],
                                            inventory[1], inventoryItemAmount[inventory[1]],
                                            null, 0,
                                            false,
                                            true,
                                            false,
                                            false);
                                        break;
                                    case 3:
                                        result = crafting.Craft(
                                            inventory[0], inventoryItemAmount[inventory[0]],
                                            inventory[1], inventoryItemAmount[inventory[1]],
                                            inventory[2], inventoryItemAmount[inventory[2]],
                                            false,
                                            true,
                                            false,
                                            false);
                                        break;
                                    default:
                                        soundEffects.WrongSound();
                                        result = null;
                                        Debug.Log("ERROR: NO RECIPE FOUND");
                                        break;
                                }
                                if (result != null)
                                {
                                    result = GetQuality(result);
                                    StartCoroutine(Cook());
                                }
                            }
                            else
                            {
                                soundEffects.WrongSound();
                            }
                        }
                    }
                    else
                    {
                        SetDoorState(true);
                    }
                }
            }
        }
    }
    private Item FindItem(int quality, Item food)
    {
        var itemManager = ItemManager.Instance;
        foreach (Item item in itemManager.AllItems)
        {
            if (item.itemQuality == quality && item.itemIndex == food.itemIndex)
            {
                return item;
            }
        }
        return null;
    }
    private Item GetQuality(Item food)
    {
        int chance = Random.Range(1, 11);
        switch (chance)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                return FindItem(1, food);
            case 6:
            case 7:
            case 8:
                return FindItem(2, food);
            case 9:
            case 10:
                return FindItem(3, food);
            default:
                return null;
        }
    }
    private IEnumerator Cook()
    {
        SetFurnaceState(true);
        InventoryList.text = string.Empty;
        SetDoorState(false);
        yield return new WaitForSeconds(3);
        SetDoorState(true);
        SetFurnaceState(false);
        Debug.Log(result.name);
        InventoryManager.Instance.Add(result);
        EmptyFurnace(false);
    }
}

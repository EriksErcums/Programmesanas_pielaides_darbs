using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private SoundEffects soundEffects;
    private bool drag = true;

    private Mortar mortar;
    private Cauldron cauldron;
    private Furnace furnace;
    private CuttingBoard cuttingBoard;

    private void Start()
    {
        soundEffects = SoundEffects.Instance;
    }
    private void Update()
    {
        if (drag)
        {
            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
            gameObject.transform.localPosition = position;
            if (!gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    public void SetDrag(bool state) { drag = state; }
    private Item GetThisItem()
    {
        return gameObject.GetComponent<ItemController>().Item;
    }
    private void ReturnToInventory()
    {
        Debug.Log("Failed inserting item");
        InventoryManager.Instance.Add(GetThisItem());
        Destroy(gameObject);
    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Vector2 position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(position, new Vector2(0f, 0f));
        if (hit)
        {
            string tag = hit.collider.tag;
            Debug.Log(hit.collider.name);
            switch (tag)
            {
                case "Mortar":
                    mortar = hit.collider.GetComponentInParent<Mortar>();
                    if (!mortar.GetActiveState())
                    {
                        if (Crafting.Instance.CheckWorkstation(gameObject.GetComponent<ItemController>().Item, true, false, false, false))
                        {
                            drag = false;
                            mortar.AddItem(GetThisItem(), gameObject);
                            gameObject.transform.position = mortar.GetPosition();
                            Sprite[] sprite = mortar.GetItemSprite(GetThisItem());
                            gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
                            gameObject.GetComponent<CircleCollider2D>().enabled = true;
                            gameObject.transform.SetParent(GameObject.Find("Environment/Mortar").transform);
                            gameObject.transform.localScale = Vector3.one;
                            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Normal game view";
                            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                        }
                        else
                        {
                            soundEffects.WrongSound();
                            ReturnToInventory();
                        }
                    }
                    else
                    {
                        ReturnToInventory();
                    }
                    break;
                case "Cauldron":
                    cauldron = hit.collider.GetComponentInParent<Cauldron>();
                    if (!cauldron.GetCauldronState())
                    {
                        if (Crafting.Instance.CheckWorkstation(gameObject.GetComponent<ItemController>().Item, false, false, false, true) && !cauldron.GetCauldronState())
                        {
                            drag = false;
                            Item item = GetThisItem();
                            cauldron.AddItem(item);
                            Destroy(gameObject);
                        }
                        else
                        {
                            soundEffects.WrongSound();
                            ReturnToInventory();
                        }
                    }
                    else
                    {
                        ReturnToInventory();
                    }
                    break;
                case "FuelSpot":
                    if(gameObject.CompareTag("Fuel"))
                    {
                        var furnaceFuel = FurnaceFuelSystem.Instance;
                        if (furnaceFuel.AddFuel())
                        {
                            drag = false;
                            Destroy(gameObject);
                        }
                        else
                        {
                            ReturnToInventory();
                        }
                    }
                    else
                    {
                        soundEffects.WrongSound();
                    }
                    break;
                case "Furnace":
                    furnace = hit.collider.GetComponentInParent<Furnace>();
                    if (!furnace.GetFurnaceState() &&
                        Crafting.Instance.CheckWorkstation(gameObject.GetComponent<ItemController>().Item, false, true, false, false))
                    {
                        if (!furnace.GetDoorState())
                        {
                            furnace.SetDoorState(true);
                        }
                        drag = false;
                        furnace.AddItem(GetThisItem());
                        Destroy(gameObject);
                    }
                    else
                    {
                        soundEffects.WrongSound();
                        ReturnToInventory();
                    }
                    break;
                case "CuttingBoard":
                    cuttingBoard = hit.collider.GetComponent<CuttingBoard>();
                    if (!cuttingBoard.GetCuttingBoardState())
                    {
                        if (cuttingBoard.ItemCheck(gameObject.GetComponent<ItemController>().Item))
                        {
                            cuttingBoard.AddItemToCuttingBoard(gameObject.GetComponent<ItemController>().Item);
                            Destroy(gameObject);
                        }
                        else
                        {
                            soundEffects.WrongSound();
                            ReturnToInventory();
                        }

                    }
                    else
                    {
                        soundEffects.WrongSound();
                        ReturnToInventory();
                    }
                    break;
                default:
                    ReturnToInventory();
                    break;
            }
        }
        else
        {
            ReturnToInventory();
        }
    }

    public void SetMortar(Mortar m) { mortar = m; }
    public void SetCauldron(Cauldron c) { cauldron = c; }

    private void OnMouseDown()
    {
        if (!drag)
        {
            if(mortar != null)
                mortar.RemoveItem();
            if (cauldron != null)
                cauldron.RemoveItem(GetThisItem());
            ReturnToInventory();
        }
    }
}

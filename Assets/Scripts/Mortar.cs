using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    private Item item;
    private GameObject itemObject;
    private Item newItem;

    private int hitAmount = 0;
    private bool activeMortar = false;

    private SoundEffects soundEffects;

    public GameObject mash;
    public List<Sprite> mortarSprites = new();
    public List<SpriteInMortar> spriteInMortar = new();

    private void Start()
    {
        soundEffects = SoundEffects.Instance;
    }
    public bool GetActiveState() { return activeMortar; }

    public Sprite[] GetItemSprite(Item item)
    {
        foreach(SpriteInMortar sprite in spriteInMortar)
        {
            if (sprite.item == item)
                return sprite.sprite;
        }
        return null;
    }
    public Color GetItemColor(Item item)
    {
        foreach (SpriteInMortar color in spriteInMortar)
        {
            if (color.item == item)
                return color.color;
        }
        return Color.white;
    }
    public void AddItem(Item newItem, GameObject newItemObject)
    {
        item = newItem;
        itemObject = newItemObject;
        gameObject.GetComponentInChildren<MortarCover>().SetItem(true);
        newItemObject.GetComponent<DraggableItem>().SetMortar(this);
    }
    public void RemoveItem()
    {
        item = null;
        Destroy(itemObject);
        itemObject = null;
        gameObject.GetComponentInChildren<MortarCover>().SetItem(false);
    }
    public Vector3 GetPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public GameObject GetItemObject()
    { 
        if(itemObject != null)
            return itemObject;
        return null;
    }
    public void ReportHit()
    {
        hitAmount++;
        if(activeMortar)
            NextAnimationFrame();
    }
    private void ResetHit() { hitAmount = 0; }
    public void StartMash()
    {
        if (itemObject != null)
        {
            itemObject.GetComponent<CircleCollider2D>().enabled = false;
            newItem = Crafting.Instance.Craft(item, 1, null, 0, null, 0, true, false, false, false);
            if(newItem != null)
            {
                ResetHit();
                activeMortar = true;
                Debug.Log(newItem.itemName);
            }
            else
            {
                soundEffects.WrongSound();
                Debug.Log("RETURN ITEM IS NULL");
            }
        }
    }
    public void PickUp()
    {
        Debug.Log(1);
        bool pestleState = gameObject.GetComponentInChildren<Pestle>().GetActivePestle();
        if(!activeMortar && !pestleState && newItem != null)
        {
            Debug.Log(2);
            Instantiate(newItem.itemPrefab);
            mash.GetComponent<SpriteRenderer>().sprite = null;
            newItem = null;
            gameObject.GetComponentInChildren<MortarCover>().SetItem(false);
            ResetHit();
        }
    }
    private void EndMash()
    {
        soundEffects.DoneSound();
        activeMortar = false;
        item = null;
        Destroy(itemObject);
        itemObject = null;
    }
    private void NextAnimationFrame()
    {
        if(mortarSprites.Count > 0 && hitAmount <= mortarSprites.Count)
        {
            mash.GetComponent<SpriteRenderer>().sprite = mortarSprites[hitAmount - 1];
            mash.GetComponent<SpriteRenderer>().color = GetItemColor(item);
            SpriteInMortar newSIM = null;
            foreach (SpriteInMortar sim in spriteInMortar)
            {
                if (sim.item == item)
                {
                    newSIM = sim;
                    break;
                }
            }
            if (spriteInMortar.Count > 0 && hitAmount - 1 < spriteInMortar[spriteInMortar.IndexOf(newSIM)].sprite.Count())
            {
                Debug.Log(spriteInMortar[spriteInMortar.IndexOf(newSIM)].sprite.Count());
                itemObject.GetComponent<SpriteRenderer>().sprite = spriteInMortar[spriteInMortar.IndexOf(newSIM)].sprite[hitAmount - 1];
            }
        }
    }
    private void Update()
    {
        if (activeMortar)
        {
            if(hitAmount > mortarSprites.Count)
            {
                EndMash();
            }
        }
    }
}

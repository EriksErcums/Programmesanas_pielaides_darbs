using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public static CuttingBoard Instance;

    //Cutting state
    private bool activeCuttingBoard = false;

    //Sprites
    public List<ItemCuttingList> cuttingList = new List<ItemCuttingList>();

    //Food spot
    public GameObject spot;

    //Currant food and sprite index
    private ItemCuttingList currantFood;
    private int spriteIndex;

    //Cutting board sprites
    public Sprite cuttingBoardWithKnife;
    public Sprite cuttingBoard;

    //Sound
    private SoundEffects soundEffects;

    //Knife
    public GameObject knife;

    //cutting splash animation
    private Animator animator;

    public void SetCuttingBoardState(bool state) { activeCuttingBoard = state;}
    public bool GetCuttingBoardState() {  return activeCuttingBoard; }

    private void Awake()
    {
        Instance = this;
        soundEffects = SoundEffects.Instance;
        animator = GetComponentInChildren<Animator>();
    }
    public bool ItemCheck(Item item)
    {
        foreach (ItemCuttingList food in cuttingList)
        {
            if (food.item == item)
            {
                spot.transform.localPosition = new Vector3(0f, food.foodHight, 0f);
                return true;
            }
        }
        return false;
    }
    public void AddItemToCuttingBoard(Item item)
    {
        foreach(ItemCuttingList food in cuttingList)
        { 
            if(food.item == item)
            {
                SetCuttingBoardState(true);
                currantFood = food;
                spriteIndex = 0;
                SetSprite();
                gameObject.GetComponent<SpriteRenderer>().sprite = cuttingBoard;

                knife.SetActive(true);
                knife.GetComponent<Knife>().ActivateKnife();
            }
        }
    }

    public void SetSprite()
    {
        var spotRenderer = spot.GetComponent<SpriteRenderer>();
        spotRenderer.sprite = currantFood.sprites[spriteIndex];
    }

    public void IncrementSpriteIndex()
    {
        spriteIndex++;
        if (spriteIndex <= currantFood.sprites.Count - 1)
        {
            SetSprite();
        }
        else if(spriteIndex > currantFood.sprites.Count - 1)
        {
            Complete();
        }
    }

    private void Complete()
    {
        soundEffects.DoneSound();
        InventoryManager.Instance.Add(currantFood.result);
        currantFood = null;
        spriteIndex = 0;
        spot.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<SpriteRenderer>().sprite = cuttingBoardWithKnife;
        SetCuttingBoardState(false);
        knife.GetComponent<Knife>().DeactivateKnife();
    }
    public void Fail()
    {
        if(spriteIndex == 0)
        {
            InventoryManager.Instance.Add(currantFood.item);
        }
        currantFood = null;
        spriteIndex = 0;
        spot.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<SpriteRenderer>().sprite = cuttingBoardWithKnife;
        SetCuttingBoardState(false);
    }

    public void PlayCutAnimation()
    {
        animator.Play("CuttingSpashAnimation", 0, 0f);
    }
}

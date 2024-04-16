using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe/Create New Recipe")]

public class Recipe : ScriptableObject
{
    public Item firstItem;
    public int firstItemAmount;
    public Item secondItem;
    public int secondItemAmount;
    public Item thirdItem;
    public int thirdItemAmount;

    public Item result;
    public int resultAmount;

    public bool mortar;
    public bool furnace;
    public bool cuttingBoard;
    public bool cauldron;
}

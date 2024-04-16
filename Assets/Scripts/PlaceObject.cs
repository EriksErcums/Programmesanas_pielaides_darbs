using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public static PlaceObject Instance;
    public bool freeSpot = true;
    public GameObject spot;

    public GameObject furnaceFuelSpot;

    private void Awake()
    {
        Instance = this;
    }
    public bool Place(Item objToPlace)
    {
        return false;
    }

}

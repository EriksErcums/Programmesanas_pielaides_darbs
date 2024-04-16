using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceFuelSystem : MonoBehaviour
{
    public static FurnaceFuelSystem Instance;
    private bool hasFuel = false;
    private int fuelAmount = 0;
    private const int fuelAddAmount = 5; //The amount of fuel that is added
    public GameObject fire;

    private SoundEffects soundEffects;
    public bool GetFuel() {  return hasFuel; }
    public int GetFuelAmount() {  return fuelAmount; }
    public bool AddFuel()
    {
        if(fuelAmount >= 1)
        {
            soundEffects.WrongSound();
            return false;
        }
        else
        {
            fuelAmount += fuelAddAmount;
            hasFuel = true;
            Debug.Log("Fuel amount: " + fuelAmount + "\n Has fuel: " + hasFuel); 
            fire.SetActive(true);
            return true;
        }
    }
    public void RemoveFuel() { fuelAmount--; if (fuelAmount <= 0) hasFuel = false; fire.SetActive(false); }
    public void ClearFuel() { fuelAmount = 0; hasFuel = false; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        soundEffects = SoundEffects.Instance;
    }
}

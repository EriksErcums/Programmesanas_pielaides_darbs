using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public static SpawnCustomer Instance;

    public GameObject customer;
    public GameObject customerParent;

    private void Awake()
    {
        Instance = this;
    }
    public void Spawn()
    {
        Instantiate(customer, customerParent.transform);
    }
}

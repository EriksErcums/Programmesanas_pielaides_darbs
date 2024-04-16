using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance;

    public GameObject ShopFront;

    //NPC
    [HideInInspector]
    private GameObject activeNPC;

    public void AddNPC(GameObject npc) { activeNPC = npc; }
    public void RemoveNPC() { activeNPC = null; }
    private void Update()
    {
        if(activeNPC != null)
        {
            if(ShopFront.activeSelf)
                activeNPC.SetActive(true);
            else 
                activeNPC.SetActive(false);
        }
    }

}

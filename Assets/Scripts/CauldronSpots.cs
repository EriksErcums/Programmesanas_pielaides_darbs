using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronSpots : MonoBehaviour
{
    public string side;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.CompareTag("Spoon"))
        {
            gameObject.GetComponentInParent<Cauldron>().ReportHit(side);
        }
    }
}

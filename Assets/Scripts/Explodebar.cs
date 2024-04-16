using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodebar : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Explode()
    {
        if(animator != null)
        animator.SetTrigger("Explode");
    }
}

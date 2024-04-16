using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarCover : MonoBehaviour
{
    private Animator animator;

    private bool activeCover = true;
    private bool mouseOverCover = false;
    private bool activePestle = false;
    private bool hasItem = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetItem(bool state)
    {
        if(state)
        {
            PlayAnimation(!state);
            hasItem = state;
        }
        else
        {
            hasItem = state;
            PlayAnimation(!state);
        }
    }
    public void SetActiveCover(bool state) { activeCover = state; }
    public bool GetActiveCover() {  return activeCover; }
    public void SetActivePestle(bool state)
    {  
        activePestle = state;
        PlayAnimation(!state);
    }

    private void Update()
    {
        if (!activePestle && !gameObject.GetComponentInParent<Mortar>().GetActiveState())
        {
            var point = Camera.main.ScreenPointToRay(Input.mousePosition);
            mouseOverCover = gameObject.GetComponent<PolygonCollider2D>().bounds.IntersectRay(point);
            if (mouseOverCover)
            {
                if (activeCover)
                {
                    PlayAnimation(false);
                    SetActiveCover(false);
                }
            }
            if (!mouseOverCover)
            {
                if (!activeCover)
                {
                    PlayAnimation(true);
                    SetActiveCover(true);
                }
            }
        }
    }
    private void OnMouseDown()
    {
        gameObject.GetComponentInParent<Mortar>().PickUp();
    }
    private void PlayAnimation(bool state)
    {
        if (!hasItem)
        {
            if(animator != null)
                animator.SetBool("ActiveCover", state);
        }
    }
}

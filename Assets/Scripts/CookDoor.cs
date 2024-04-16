using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookDoor : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CloseDoor()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        animator.SetTrigger("CloseDoor");
        GameObject.Find("UI Elements/FurnaceInventory").SetActive(false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void OpenDoor()
    {
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        animator.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
}

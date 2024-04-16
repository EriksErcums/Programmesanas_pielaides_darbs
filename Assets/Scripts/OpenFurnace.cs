using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFurnace : MonoBehaviour
{
    private Animator animator;
    private SoundEffects soundEffects;
    private ClickObject clickObject;
    private void Start()
    {
        clickObject = ClickObject.Instance;
        animator = GetComponentInChildren<Animator>();
        soundEffects = SoundEffects.Instance;
    }
    public void OpenDoor()
    {
        gameObject.GetComponent<Button>().enabled = false;
        StartCoroutine(Open());
    }
    private IEnumerator Open()
    {
        clickObject.canOpen = false;
        soundEffects.DoorSound();
        animator.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.GetComponent<ClickObject>().OpenMenus();
        clickObject.canOpen = true;
    }
    private IEnumerator Close()
    {
        clickObject.canOpen = false;
        soundEffects.DoorSound();
        animator.SetTrigger("CloseDoor");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.GetComponent<Button>().enabled = true;
        clickObject.canOpen = true;
    }

    public void ClsoeDoor()
    {
        StartCoroutine(Close());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMortal : MonoBehaviour
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
    public void OpenMortalMenu()
    {
        gameObject.GetComponent<Button>().enabled = false;
        StartCoroutine(Open());
    }
    private IEnumerator Open()
    {
        clickObject.canOpen = false;
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 2);
        soundEffects.PestleSound();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 2);
        gameObject.GetComponent<Button>().enabled = true;
        clickObject.canOpen = false;
        gameObject.GetComponent<ClickObject>().OpenMenus();
    }
}

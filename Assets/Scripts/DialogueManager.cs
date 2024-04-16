using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;

public class DialogueManager : MonoBehaviour
{
    public Animator textBoxAnimator;
    public Animator spriteAnimator;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueTextBox;
    public GameObject shopTextBox;

    [HideInInspector]
    public bool canIncrement = false;

    public void StartDialogue()
    {
        shopTextBox.SetActive(true);
        gameObject.GetComponent<Customer>().SetRequest();
    }
}

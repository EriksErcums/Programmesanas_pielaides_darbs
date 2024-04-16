using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public void TriggerDialogue()
    {
        gameObject.GetComponent<DialogueManager>().StartDialogue();
    }
}

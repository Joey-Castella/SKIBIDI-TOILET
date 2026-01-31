using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        // OLD: FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
        // NEW (Fixes the warning):
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }


    
}
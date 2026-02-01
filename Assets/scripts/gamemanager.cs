using UnityEngine;

public class gamemanager : MonoBehaviour
{
    // 1. Drag your Dialogue Object (or type text in Inspector) here
    public Dialogue winDialogue; 

    // This function will be called by the Player
    public void TriggerWin()
    {
        Debug.Log("Triggering Win Dialogue...");
        
        // Find the Dialogue Manager and tell it to play the text
        FindFirstObjectByType<DialogueManager>().StartDialogue(winDialogue);
    }
}
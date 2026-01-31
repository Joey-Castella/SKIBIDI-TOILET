using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    public string GoneID { get; private set; }
    public GameObject itemPrefab;
    
    // 1. Add this variable to hold the text for this specific item
    public Dialogue dialogue; 

    void Start()
    {
        GoneID = GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        // 2. TRIGGER DIALOGUE HERE
        // Find the manager and pass it the dialogue data from this chest
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);

        // 3. Drop loot (if any)
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        // 4. Destroy the object
        Debug.Log($"Object {GoneID} has been removed.");
        Destroy(gameObject);
    }
}
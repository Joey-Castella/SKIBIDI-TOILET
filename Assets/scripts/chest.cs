using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    // Properties
    public string GoneID { get; private set; }
    public GameObject itemPrefab; // Optional: Item that drops before it disappears

    // Start is called before the first frame update
    void Start()
    {
        // We use your GlobalHelper here to fill in the blank from the image
        GoneID = GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        // It can always be interacted with as long as it exists
        return true;
    }

    public void Interact()
    {
        // 1. Drop loot if assigned
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        // 2. Make it GONE
        Debug.Log($"Object {GoneID} has been removed.");
        Destroy(gameObject);
    }
}
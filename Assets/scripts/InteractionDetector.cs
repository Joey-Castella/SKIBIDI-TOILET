using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; // Closest Interactable
    public GameObject interactionIcon;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the icon (e.g., "Press E") at the start
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // This function is called by the Player Input component
        if (context.performed)
        {
            // If we have an object in range, call its Interact method
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When we walk into something, check if it is "Interactable"
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true); // Show the "Press E" icon
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When we walk away, check if it was the object we were targeting
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false); // Hide the icon
        }
    }
}
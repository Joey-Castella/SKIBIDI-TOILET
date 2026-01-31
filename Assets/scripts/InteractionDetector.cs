using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; 
    public GameObject interactionIcon;

    // --- NEW VARIABLES ---
    private int collectedCount = 0;   // Tracks how many you have
    public int goalAmount = 5;        // How many you need to win
    // ---------------------

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // If there is an object nearby
            if (interactableInRange != null)
            {
                // 1. Trigger the interaction (Chest disappears)
                interactableInRange.Interact();

                // 2. Count the item
                collectedCount++;
                Debug.Log($"Collected: {collectedCount} / {goalAmount}");

                // 3. Check for Win
                if (collectedCount >= goalAmount)
                {
                    WinGame();
                }
            }
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN! Game Over.");
        // Add code here to show a Win Screen or load the next level
        // Example: UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false); 
        }
    }
}
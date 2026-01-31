using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; 
    public GameObject interactionIcon;

    private int collectedCount = 0;   
    public int goalAmount = 5;        

    // --- CHANGE IS HERE ---
    // Only 1 variable now. Drag the actual Enemy from your Hierarchy here.
    public GameObject existingEnemy; 
    // ----------------------

    void Start()
    {
        interactionIcon.SetActive(false);

        // Optional: Hide the enemy automatically when the game starts
        if (existingEnemy != null)
        {
            existingEnemy.SetActive(false);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactableInRange != null)
            {
                interactableInRange.Interact();
                collectedCount++;
                Debug.Log($"Collected: {collectedCount} / {goalAmount}");

                // --- WAKE UP THE ENEMY ---
                // If we have collected 2 items (or change this to 1 if you want)
                if (collectedCount == 2) 
                {
                    ActivateEnemy();
                }
                // -------------------------

                if (collectedCount >= goalAmount)
                {
                    WinGame();
                }
            }
        }
    }

    void ActivateEnemy()
    {
        if (existingEnemy != null)
        {
            existingEnemy.SetActive(true); // Turn him on!
            Debug.Log("⚠️ The Enemy has awakened!");
        }
        else
        {
            Debug.Log("No enemy assigned to wake up.");
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting; // <--- NEEDED FOR RESTART

public class InteractionDetector : MonoBehaviour
{
    // ... (Your existing variables) ...
    private IInteractable interactableInRange = null; 
    public GameObject interactionIcon;
    private int collectedCount = -1;   
    public int goalAmount = 5;
    public TextMeshProUGUI countText; 
    public GameObject existingEnemy; 

    // --- NEW GAME OVER VARIABLES ---
    public GameObject gameOverScreen; // Drag your Game Over Panel here
    // -------------------------------

    void Start()
    {
        // ... (Your existing Start code) ...
        interactionIcon.SetActive(false);
        if (existingEnemy != null) existingEnemy.SetActive(false);
        if (countText != null) countText.gameObject.SetActive(false);
        
        // Hide Game Over screen at start
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        
        // Make sure game time is running (in case we died before)
        Time.timeScale = 1f; 
    }

    // ... (Keep OnInteract, UpdateUI, ActivateEnemy exactly the same) ...
    public void OnInteract(InputAction.CallbackContext context)
    {
        // (Paste your existing OnInteract code here)
        // Only including this so you know where it goes. 
        // Don't delete your existing code!
        if (context.performed)
        {
             // ... logic ...
             if (interactableInRange != null)
             {
                 interactableInRange.Interact();
                 collectedCount++;
                 
                 if (collectedCount == 0 && countText != null) countText.gameObject.SetActive(true);
                 
                 UpdateUI();

                 if (collectedCount == 1) ActivateEnemy();
                 if (collectedCount >= goalAmount) WinGame();
             }
        }
    }

    // --- NEW GAME OVER FUNCTION ---
    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        
        // 1. Show the Game Over Screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // 2. Stop the game (Freeze time)
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // -----------------------------

    public void Quit()
    {
        Application.Quit();
    }

    // ... (Keep the rest of your functions: WinGame, OnTriggerEnter, etc.) ...
     void UpdateUI() { if (countText != null) countText.text = "Items: " + collectedCount + " / " + goalAmount; }
     void ActivateEnemy() { if (existingEnemy != null) existingEnemy.SetActive(true); }
     void WinGame() { if (countText != null) countText.text = "YOU ESCAPED!"; }
     private void OnTriggerEnter2D(Collider2D collision) { if(collision.TryGetComponent(out IInteractable i) && i.CanInteract()) { interactableInRange = i; interactionIcon.SetActive(true); } }
     private void OnTriggerExit2D(Collider2D collision) { if (collision.TryGetComponent(out IInteractable i) && i == interactableInRange) { interactableInRange = null; interactionIcon.SetActive(false); } }
}
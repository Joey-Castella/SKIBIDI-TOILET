using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement; 

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; 
    public GameObject interactionIcon;
    private int collectedCount = -1;   
    public int goalAmount = 5;
    public TextMeshProUGUI countText; 
    public GameObject existingEnemy; 
    public GameObject gameOverScreen; 

    // --- NEW WIN VARIABLE ---
    public GameObject winScreenParent; // Drag the PARENT object of your 5 slides here
    // ------------------------

    void Start()
    {
        interactionIcon.SetActive(false);
        if (existingEnemy != null) existingEnemy.SetActive(false);
        if (countText != null) countText.gameObject.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        
        // Hide the Win Screen at start too!
        if (winScreenParent != null) winScreenParent.SetActive(false);

        Time.timeScale = 1f; 
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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

    // --- UPDATED WIN FUNCTION ---
    void WinGame()
    {
        Debug.Log("YOU WIN!");

        // 1. Hide the gameplay UI (so it doesn't block the slides)
        if (countText != null) countText.gameObject.SetActive(false);
        if (interactionIcon != null) interactionIcon.SetActive(false);

        // 2. Turn on the Win Slides
        if (winScreenParent != null)
        {
            winScreenParent.SetActive(true);
            Time.timeScale = 0f; // Freeze game
        }
    }
    // -----------------------------

    public void GameOver()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    void UpdateUI() { if (countText != null) countText.text = "Items: " + collectedCount + " / " + goalAmount; }
    void ActivateEnemy() { if (existingEnemy != null) existingEnemy.SetActive(true); }
    private void OnTriggerEnter2D(Collider2D collision) { if(collision.TryGetComponent(out IInteractable i) && i.CanInteract()) { interactableInRange = i; interactionIcon.SetActive(true); } }
    private void OnTriggerExit2D(Collider2D collision) { if (collision.TryGetComponent(out IInteractable i) && i == interactableInRange) { interactableInRange = null; interactionIcon.SetActive(false); } }
}
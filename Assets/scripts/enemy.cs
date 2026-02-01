using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform target; 
    NavMeshAgent agent;
    public SpriteRenderer spriteRenderer; 
    
    // We don't drag this anymore. The code finds it.
    private GameObject gameOverScreen; 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 1. FIND THE PLAYER
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) target = playerObject.transform;

        // 2. FIND THE PANEL AUTOMATICALLY
        // We look for the "Canvas" first, then look for the "GameOverPanel" inside it.
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Transform panelTrans = canvas.transform.Find("GameOverPanel");
            if (panelTrans != null)
            {
                gameOverScreen = panelTrans.gameObject;
            }
        }
        
        if (gameOverScreen == null) Debug.LogError("Could not find 'GameOverPanel'! Check the spelling.");
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            if (agent.velocity.x > 0.1f) spriteRenderer.flipX = false; 
            else if (agent.velocity.x < -0.1f) spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForPlayer(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForPlayer(collision.gameObject);
    }

    void CheckForPlayer(GameObject hitObject)
    {
        if (hitObject.CompareTag("Player"))
        {
            Debug.Log("CAUGHT YOU!");

            // 3. SHOW THE PANEL
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f; // Freeze game
            }
        }
    }
}
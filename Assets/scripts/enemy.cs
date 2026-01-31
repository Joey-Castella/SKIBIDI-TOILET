using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // We can hide this from the Inspector since we find it automatically
    private Transform target; 

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // --- AUTOMATICALLY FIND PLAYER ---
        // This looks for any object tagged "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Enemy could not find the Player! Did you forget to tag the Player object?");
        }
    }

    private void Update()
    {
        // Only move if we actually found a target
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
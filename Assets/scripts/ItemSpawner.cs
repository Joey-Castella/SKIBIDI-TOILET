using UnityEngine;
using UnityEngine.AI; // Needed for NavMesh

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;   // Drag your "Gone" chest here
    public float radius = 10f;      // How far to search for a spot
    public int amountToSpawn = 5;   // How many items to spawn

    void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        // 1. Pick a random point inside a circle
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 searchPos = transform.position + new Vector3(randomPoint.x, randomPoint.y, 0);

        // 2. Ask NavMesh: "Is there a floor near this random point?"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(searchPos, out hit, 2.0f, NavMesh.AllAreas))
        {
            // 3. If yes, spawn the item at the VALID point (hit.position)
            Instantiate(itemPrefab, hit.position, Quaternion.identity);
        }
        else
        {
            // If we missed the floor, try again (recursion)
            SpawnItem();
        }
    }
    
    // Draw a circle in the Editor so you can see the spawn zone
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

    using UnityEngine;

public class VaccinePickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            VaccineManager.instance.AddVaccine();
            Destroy(gameObject);
        }
    }
} 


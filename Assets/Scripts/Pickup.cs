using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the playerstats component from the player GameObject
            playerstats stats = other.GetComponent<playerstats>();
            if (stats != null)
            {
                // Increase the HealItems float in playerstats
                stats.HealItems++;

                // Log the current number of heal items
                GameManager.LogHealItems();

                // Destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
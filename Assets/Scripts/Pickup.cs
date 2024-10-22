using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerstats stats = other.GetComponent<playerstats>();
            if (stats != null)
            {
                stats.HealItems++;

                GameManager.LogHealItems();

                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static global variable for healing items
    public static int healItems = 0;

    // Optionally, you can reset or log the number of heal items
    void Start()
    {
        healItems = 0;  // Reset at the start of the game
        Debug.Log("Heal items: " + healItems);
    }

    // Optional method to log the number of heal items
    public static void LogHealItems()
    {
        Debug.Log("Heal items: " + healItems);
    }
}
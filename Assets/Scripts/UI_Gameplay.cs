using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class UI_Gameplay : MonoBehaviour
{
    public TimeChange timeChangeScript;  // Reference to the TimeChange script
    public TextMeshProUGUI timeText;     // TextMeshPro UI element for the time
    public TextMeshProUGUI healthText;  // TextMeshPro UI element for the player's health
    public TextMeshProUGUI healItemsText; // TextMeshPro UI element for healing items

    public playerstats playerStats; // Reference to the playerstats script

    void Update()
    {
        // Update time display
        if (timeChangeScript != null && timeText != null)
        {
            timeText.text = timeChangeScript.timeString;
        }

        // Update health display
        if (playerStats != null && healthText != null)
        {
            healthText.text = $"Health = {playerStats.health}";
        }

        // Update healing items display
        if (playerStats != null && healItemsText != null)
        {
            healItemsText.text = $"HealItems = {playerStats.HealItems}";
        }
    }
}
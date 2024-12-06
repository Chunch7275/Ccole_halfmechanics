using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    private float rotationDuration = 1200f; // Duration for a full rotation
    private float currentTime = 5 * 60; // Base time in minutes (5:00 AM)
    public string timeString; // Holds the time in AM/PM format

    void Update()
    {
        float rotationSpeed = 360f / rotationDuration; // Degrees per second
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        transform.Rotate(rotationThisFrame, 0f, 0f);

        // Update currentTime based on elapsed time
        currentTime += Time.deltaTime * (1440f / rotationDuration); // 1440 minutes in a day

        if (currentTime >= 1440f) // Reset after a full day
        {
            currentTime -= 1440f;
        }

        // Convert currentTime to hours and minutes
        int hours = (int)currentTime / 60;
        int minutes = (int)currentTime % 60;

        // Determine AM/PM and adjust hours
        string period = hours >= 12 ? "PM" : "AM";
        hours = hours % 12;
        if (hours == 0) hours = 12; // Adjust 0 hour to 12 for AM/PM format

        // Format time string
        timeString = string.Format("{0}:{1:00} {2}", hours, minutes, period);
    }
}
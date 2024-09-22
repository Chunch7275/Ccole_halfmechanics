using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    private float rotationDuration = 1200f;
    void Update()
    {
        float rotationSpeed = 360f / rotationDuration; 
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        transform.Rotate(rotationThisFrame, 0f, 0f);
    }
}
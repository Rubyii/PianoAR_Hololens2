using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public float keyPressThreshold = 0.1f;  // Adjust this threshold based on your preference

    private void Start()
    {
        // Ensure the PianoKey GameObject has a collider (e.g., BoxCollider) and is tagged as "PianoKey"
        gameObject.tag = "WhiteKey";
    }

    public void TriggerKeyPress(float keyPressAmount)
    {
        // Adjust the key behavior based on the key press amount
        if (keyPressAmount > keyPressThreshold)
        {
            // Key is pressed, trigger your desired action (e.g., play a sound, change color)
            Debug.Log("Key pressed");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoControllerCollision : MonoBehaviour
{
    public GameObject virtualPiano;  // Reference to the virtual piano GameObject
    public float keyPressThreshold = 0.05f;  // Adjust this threshold based on your preference

    private void Update()
    {
        CheckKeyPresses();
    }

    private void CheckKeyPresses()
    {
        // Assuming you have fingertip GameObjects as child objects of the hand
        foreach (Transform fingertip in transform)
        {
            // Check for collisions with piano keys
            Collider[] colliders = Physics.OverlapSphere(fingertip.position, 0.01f);  // Adjust the radius as needed
            foreach (Collider collider in colliders)
            {
                // Check if the collided object is a piano key
                if (collider.CompareTag("PianoKey"))
                {
                    // Get the PianoKey component and trigger the key press
                    PianoKey pianoKey = collider.GetComponent<PianoKey>();
                    if (pianoKey != null)
                    {
                        float keyPressAmount = 1.0f - Mathf.Clamp01(Vector3.Distance(fingertip.position, collider.transform.position) / keyPressThreshold);
                        pianoKey.TriggerKeyPress(keyPressAmount);
                    }
                }
            }
        }
    }
}

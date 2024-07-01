using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNoteScript : MonoBehaviour
{
    // Adjust these parameters based on your visual preferences
    public float fallSpeed = 5f;
    public Color initialColor = Color.white;
    public Color finalColor = Color.blue;

    private float noteLength;

    // Set the length of the note based on the simulation or MIDI duration
    public void SetNoteLength(float duration)
    {
        noteLength = duration*0.5f;
    }

    void Start()
    {
        // Adjust the color and size of the note based on the set parameters
        GetComponent<Renderer>().material.color = initialColor;
        transform.localScale = new Vector3(-0.003532861f, noteLength, 7.436745e-05f);
    }

    void Update()
    {
        // Move the note downward at a constant speed
        // Destroy the note after a certain time (adjust as needed)

        //if(falling pressed)
        //{transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);}
        //else
        //{wait}


        
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Interpolate the color based on the note's position
        float t = Mathf.Clamp01((noteLength - transform.position.y) / noteLength);
        GetComponent<Renderer>().material.color = Color.Lerp(initialColor, finalColor, t);

        // Destroy the note when it goes below a certain position
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}

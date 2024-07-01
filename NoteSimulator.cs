using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;

public class NoteSimulator : MonoBehaviour
{
    // Define a sequence of notes with pitch and duration
    //public float[][] pieces;
    public float fallSpeed = 0.5f;
    public float offset_fallingnote_y_gap;
    private int[] simulatedPitches = { 
        43, 44, 46, 44, 43, 44, 46, 43, 44, 46, 46, 46, 46,
        48, 39, 39, 41, 43, 39, 41, 43, 44, 43, 43, 41, 41, 39, 41, 43, 44,
        46, 44, 43, 44, 46 ,43 ,44, 46, 46, 46, 46,
        48, 39, 39, 41, 43, 39, 41, 43, 44, 43, 43, 41, 55, 56, 58, 58, 55, 56,
        58, 55, 56, 58, 58, 58, 58, 60, 60, 51, 51, 53,
        55, 53, 51 ,53, 55, 56, 55, 55, 53, 55, 56,
        55, 56, 58, 58, 55, 56,
        58, 55, 56, 58, 58, 58, 58, 60, 60, 51, 51, 53,
        55, 53, 51 ,53, 55, 56, 55, 55, 53, 51, 48, 51, 55, 58, 55, 56, 55, 51,

    };
    private float[] simulatedDurations = {
        1f,   1f,   1.5f, 0.5f, 1f,   1f,   2f,   1f,   1f,   1.5f, 0.5f, 1f,   1f,
        1f,   1f,   1f,   1f,   2f,   1f,   1f,   2f,   1f,   1f,   1f,   1f,   1f,   1f,   2f,   1f,   1f,
        1.5f, 0.5f, 1f,   1f,   2f,   1f,   1f,   1.5f, 0.5f, 1f,   1f,
        1f,   1f,   1f,   1f,   2f,   1f,   1f,   2f,   1f,   1f,   1f,    1f,   1f,   1f,   1f,   1f,   1f,
        2f,   1f,   1f,   1.5f, 0.5f, 1f,   1f,   0.5f, 0.5f, 1f,   1f,   1f,
        1f,   1f,   1f,   1f,   2f,   1f,   1f,   1f,   5f,   1f,   1f,
        1f,   1f,   1f,   1f,   1f,   1f,
        2f,   1f,   1f,   1.5f, 0.5f, 1f,   1f,   0.5f, 0.5f, 1f,   1f,   1f,
        1f,   1f,   1f,   1f,   2f,   1f,   1f,   1f,   1f,   1f,   1f,   3f,   1f,   1f,   1f,   1f,   1f, 1f,  4f, 1f
    };
    private int[] blackKeys = { 1, 4, 6, 9, 11, 13, 16, 18, 21, 23, 25, 28, 30, 33, 35, 37, 40, 42, 45, 47, 49, 52, 54, 57, 59, 61, 64, 66, 69, 71, 73, 76, 78, 81, 83, 85 };
    private int[] whiteKeys = { 0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 20, 22, 24, 26, 27, 29, 31, 32, 34, 36, 38, 39, 41, 43, 44, 46, 48, 50, 51, 53, 55, 56, 58, 60, 62, 63, 65, 67, 68, 70, 72, 74, 75, 77, 79, 80, 82, 84, 86, 87  };

    public GameObject fallingNotePrefab;
    public GameObject piano;
    public GameObject deathPlane;
    public GameObject[] pianoKeys;
    public GameObject[] spawnPoints;
    public List<GameObject> flyingNotesPiece;

    [SerializeField]
    private new bool enabled;
    [SerializeField]
    private bool anyHit;
    [SerializeField]
    private GameObject pianoKeyHitted;
    [SerializeField]
    private GameObject flyingNoteHitted;
    [SerializeField]
    private ColliderPianoKeys pianoKeyHittedScript;
    private Collider collider_deathplane;
    private float nextSpawnpointY;



    void Start()
    {
        // Start simulating notes
        //StartCoroutine(SimulateNotes());
        //pieces = new float[][] { simulatedPitches, simulatedDurations };
        anyHit = false;
        pianoKeyHitted = null;
        flyingNoteHitted = null; 
        pianoKeyHittedScript = null;
        //SpawnPiece(simulatedPitches,simulatedDurations);
        enabled = gameObject.activeInHierarchy;
        collider_deathplane = deathPlane.GetComponent<Collider>();
        Transform deathPlaneTransform = deathPlane.transform;
        float newY = 0;
        foreach (float duration in simulatedDurations)
        {
            newY -= duration * 0.15f;
        }
        deathPlaneTransform.position = new Vector3(deathPlaneTransform.position.x, newY, deathPlaneTransform.position.z);

        Debug.Log(simulatedPitches.Length);
        Debug.Log(simulatedDurations.Length);

        if (simulatedPitches.Length == simulatedDurations.Length)
        {
            Debug.Log("LAAAAAAAAAAAAANGE GLEICH");
        }
    }

    public void OnEnable()
    {
        Debug.Log("ONENABLE");
        /*
        foreach (GameObject pianokey in pianoKeys)
        {
            ColliderPianoKeys targetScript = pianokey.GetComponent<ColliderPianoKeys>();
            if (!targetScript.enable)
            {
                Debug.Log("ENABLE AN");
            }
        }
        */
        enabled = true;
        nextSpawnpointY = 0;
        anyHit = false;
        pianoKeyHitted = null;
        flyingNoteHitted = null;
        pianoKeyHittedScript = null;
        SpawnPiece(simulatedPitches, simulatedDurations);
    }

    public void OnDisable()
    {
        enabled = false;
        foreach (GameObject flyingNote in flyingNotesPiece)
        {
            if (flyingNote != null)
            {
                Destroy(flyingNote);
            }
        }
        flyingNotesPiece.Clear();
    }

    private void Update()
    {
        if (!enabled)
        {
            return;
        }
        /*
        foreach (GameObject pianoKey in pianoKeys)
        {

            if (pianoKey != null)
            {
                ColliderPianoKeys targetScript = pianoKey.GetComponent<ColliderPianoKeys>();
                if (targetScript.isPressed)
                {
                    noteHitsKey = true;
                }
                Debug.Log(targetScript.isPressed);
            }
            else
            {
                Debug.LogError("One of the targetObjects is not assigned.");
            }  
        }
        */

        //Debug.Log(noteHitsKey);

        if (flyingNotesPiece == null)
        {
            return;
        } else
        {
            foreach (GameObject flyingNote in flyingNotesPiece)
            {
                Collider collider_flyingNote = flyingNote.GetComponent<Collider>();

                if (!anyHit)
                {
                    flyingNote.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                    //Debug.Log("Move Down");
                    //noteHitsKey = true;
                    //continue all movement
                }

                if (collider_flyingNote.bounds.Intersects(collider_deathplane.bounds))
                {
                    flyingNotesPiece.Remove(flyingNote);
                    Destroy(flyingNote);
                }
                GameObject pianoKey_flyingNote = pianoKeys[simulatedPitches[flyingNotesPiece.IndexOf(flyingNote)]];
                Collider collider_pianoKey = pianoKey_flyingNote.GetComponent<Collider>();

                if (collider_flyingNote != null && collider_pianoKey != null && !anyHit)
                {

                    // Check if the bounds of the colliders are overlapping
                    anyHit = collider_flyingNote.bounds.Intersects(collider_pianoKey.bounds);

                    if (anyHit)
                    {
                        pianoKeyHitted = pianoKey_flyingNote;
                        pianoKeyHittedScript = pianoKey_flyingNote.GetComponent<ColliderPianoKeys>();
                        flyingNoteHitted = flyingNote;
                        Debug.Log("HIT!!!!!!!!!");
                    }

                }

                if (collider_flyingNote != null && anyHit)
                {
                    //abfrage wofuer??
                    int IndexOfPianoKey;
                    if (pianoKeyHitted.CompareTag("WhiteKey"))
                    {
                        //Debug.Log("Weiss");
                        IndexOfPianoKey = Array.IndexOf(pianoKeys, pianoKeyHitted);
                    }
                    else if (pianoKeyHitted.CompareTag("BlackKey"))
                    {
                        //Debug.Log("Schwarz");
                        IndexOfPianoKey = Array.IndexOf(pianoKeys, pianoKeyHitted);
                    }

                    //whiteKeys.Contains(simulatedPitches[flyingNotesPiece.IndexOf(flyingNote)]);

                    if (pianoKeyHitted != null && flyingNoteHitted != null)
                    {
                        int valueOfPianoKeyHitted = Array.IndexOf(pianoKeys, pianoKeyHitted);
                        int valueOfStoppedNote = simulatedPitches[flyingNotesPiece.IndexOf(flyingNoteHitted)];


                        if (pianoKeyHittedScript != null && pianoKeyHittedScript.isPressed)
                        {
                            anyHit = false;
                            Debug.Log("ISPRESSED");
                        }
                        else if (valueOfStoppedNote != valueOfPianoKeyHitted)
                        {
                            anyHit = false;
                        }
                    }
                }
                /*
                foreach (GameObject pianoKey in pianoKeys)
                {
                    Collider collider_pianoKey = pianoKey.GetComponent<Collider>();
                    
                    if (collider_flyingNote != null && collider_pianoKey != null && !anyHit)
                    {

                        // Check if the bounds of the colliders are overlapping
                        anyHit =  collider_flyingNote.bounds.Intersects(collider_pianoKey.bounds);

                        if (anyHit)
                        {
                            pianoKeyHitted = pianoKey;
                            pianoKeyHittedScript = pianoKeyHitted.GetComponent<ColliderPianoKeys>();
                            flyingNoteHitted = flyingNote;
                            Debug.Log("HIT!!!!!!!!!");
                        }

                    }

                    if (collider_flyingNote != null && anyHit)
                    {
                        //abfrage wofuer??
                        int IndexOfPianoKey;
                        if (pianoKeyHitted.CompareTag("WhiteKey"))
                        {
                            //Debug.Log("Weiss");
                            IndexOfPianoKey = Array.IndexOf(pianoKeys, pianoKeyHitted);
                        }
                        else if (pianoKeyHitted.CompareTag("BlackKey"))
                        {
                            //Debug.Log("Schwarz");
                            IndexOfPianoKey = Array.IndexOf(pianoKeys, pianoKeyHitted);
                        }

                        //whiteKeys.Contains(simulatedPitches[flyingNotesPiece.IndexOf(flyingNote)]);

                        if (pianoKeyHitted != null && flyingNoteHitted != null)
                        {
                            int valueOfPianoKeyHitted = Array.IndexOf(pianoKeys, pianoKeyHitted);
                            int valueOfStoppedNote = simulatedPitches[flyingNotesPiece.IndexOf(flyingNoteHitted)];


                            if(pianoKeyHittedScript!= null && pianoKeyHittedScript.isPressed)
                            {
                                anyHit = false;
                                Debug.Log("ISPRESSED");
                            }
                            else if (valueOfStoppedNote != valueOfPianoKeyHitted)
                            {
                                anyHit = false;
                            }
                        }
                    }
                */
                }

    


                if (anyHit)
                {
                    //Debug.Log("HIT!!!!!!!!!!!!!!!");
                }


                

                //Debug.Log(flyingNotesPiece.IndexOf(flyingNote));
                /*
                if (noteHitsKey)
                {
                    //Stop all movement
                    MoveStop(flyingNote);
                }
                else if(!noteHitsKey)
                {
                    flyingNote.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                    //Debug.Log("Move Down");
                    //noteHitsKey = true;
                    //continue all movement
                }
                */
            }
        }
    

    public void SpawnPiece(int[] PitchArray, float[] DurationArray)
    {
        PitchArray = simulatedPitches;
        DurationArray = simulatedDurations;

        for (int i = 0; i < PitchArray.Length; i++)
        {
            GameObject fallingNote;
            // Calculate the spawn position based on the MIDI pitch
            Vector3 spawnPosition = spawnPoints[PitchArray[i]].transform.position;
            spawnPosition.y = spawnPosition.y + nextSpawnpointY;

            // Spawn a falling note at the corresponding position
            Quaternion orientation = pianoKeys[PitchArray[i]].transform.rotation;

            orientation *= Quaternion.Euler(Vector3.right * 270);
            //Debug.Log(orientation.eulerAngles);

            fallingNote = Instantiate(fallingNotePrefab, spawnPosition, orientation);

            fallingNote.transform.localScale = new Vector3(0.0075f, DurationArray[i]*0.1f, 7.436745e-05f);

            nextSpawnpointY += (fallingNote.transform.localScale.y/2)+ (DurationArray[i+1] * 0.1f / 2)+ offset_fallingnote_y_gap;

            //Debug.Log("Laenge: " + (DurationArray[i] * 0.1f)/2);
            //Debug.Log("NEXTSPAWN POINT: "+nextSpawnpointY);


            if (whiteKeys.Contains(simulatedPitches[i]))
            {

                // Change the color of the instantiated object
                Renderer renderer = fallingNote.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.blue; // Change to the desired color
                }
                else
                {
                    UnityEngine.Debug.LogError("Renderer component not found on instantiated object.");
                }
            }
            else
            {
                Renderer renderer = fallingNote.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red; // Change to the desired color
                }
                else
                {
                    UnityEngine.Debug.LogError("Renderer component not found on instantiated object.");
                }
            }
            //fallingNote.transform.localScale.Set(-0.01f, DurationArray[i], 7.436745e-05f);    

            // Adjust note behavior based on the simulated duration
            //fallingNote.GetComponent<FallingNoteScript>().SetNoteLength(DurationArray[i]);

            //Debug.Log("Wiederholung: "+i);
            //Debug.Log("GameObject: "+fallingNote);
            //iteration.Add(i);
            //Debug.Log("WiederholungList: " + iteration);
            flyingNotesPiece.Add(fallingNote);
            //Thread.Sleep((int)DurationArray[i]*1000);
        }
        //Debug.Log("GAMEOBJECT LIST");
        //Debug.Log(flyingNotesPiece);
    }

    System.Collections.IEnumerator SimulateNotes()
    {
        for (int i = 0; i < simulatedPitches.Length; i++)
        {
            // Trigger a falling note based on the simulated data
            TriggerNote(simulatedPitches[i], simulatedDurations[i]);

            // Wait for the specified duration before triggering the next note
            yield return new WaitForSeconds(simulatedDurations[i]);
        }
    }

    void TriggerNote(int pitch, float duration)
    {
        // Calculate the spawn position based on the MIDI pitch
        Vector3 spawnPosition = CalculateSpawnPosition(pitch, duration);

        // Spawn a falling note at the corresponding position
        GameObject fallingNote = Instantiate(fallingNotePrefab, spawnPosition, Quaternion.identity);

        // Adjust note behavior based on the simulated duration
        fallingNote.GetComponent<FallingNoteScript>().SetNoteLength(duration);

        StartCoroutine(MoveDown(fallingNotePrefab));

        // Destroy the note after a certain time (adjust as needed)
        //if(falling note not pressed)
        //{Wait}
        //else
        //Destroy(fallingNote, duration + 2f);
    }

    private IEnumerator MoveDown(GameObject note)
    {
        float elapsedTime = 0f;

        while (true/*!note collide with triggerplane*/)
        {
            // Move the note upward
            note.transform.Translate(Vector3.up * fallSpeed * Time.deltaTime);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Destroy the note after its lifetime
        //Destroy(note);
    }

    private void MoveStop(GameObject note)
    {
        note.transform.Translate(Vector3.zero);
    }

    private void MoveContinue(GameObject note)
    {
        note.transform.Translate(Vector3.up * fallSpeed);
    }

    Vector3 CalculateSpawnPosition(int pitch, float duration)
    {
        float xPosition = spawnPoints[pitch].transform.position.x;
        float yPosition = spawnPoints[pitch].transform.position.y + duration * 0.5f;
        float zPosition = spawnPoints[pitch].transform.position.z;

        return new Vector3(xPosition, yPosition+0.1f, zPosition);
    }
}

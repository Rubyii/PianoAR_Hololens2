using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ColliderPianoKeys : MonoBehaviour
{
    public GameObject notePrefab; // The prefab of the note you want to spawn
    public float spawnInterval = 0.01f; // Time interval between note spawns
    public float moveSpeed = 0.1f; // Speed at which the notes move upward
    public float lifetime = 3.0f; // Lifetime of the notes
    public float z_axis_spawn_offset;
    public float x_axis_spawn_offset;
    public bool isPressed;
    public GameObject spawnPoint;
    public float FadeTime;

    private Coroutine spawnCoroutine;
    public bool enable;

    private AudioSource audioSource;
    public static float startVolume;

    public IEnumerator fadeSound1;


    private void Start()
    {
        enable = gameObject.activeInHierarchy;
        spawnInterval = 0.01f;
        z_axis_spawn_offset = 0f;
        x_axis_spawn_offset = 0f;
        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
        //fadeSound1 = FadeOut(audioSource, FadeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sphere"))
        {
            isPressed = true;
           //UnityEngine.Debug.Log("ONTRIGGERENTER");
            if (spawnCoroutine == null && enable)
            {
                spawnCoroutine = StartCoroutine(SpawnAndMoveNotes());
            }
        }
        /*
        if (!enable)
        {
            return;
        }
        */
        //UnityEngine.Debug.Log("ONTRIGGERENTEROHNEIF");
    }

    private void OnTriggerExit(Collider other)
    {
        //UnityEngine.Debug.Log("ONTRIGGEREXITOHNEIF");
        if (other.CompareTag("sphere"))
        {

            isPressed = false;
            //StartCoroutine(fadeSound1);
            //StopCoroutine(fadeSound1);
            //UnityEngine.Debug.Log("ONTRIGGEREXIT");
            if (spawnCoroutine != null && enable)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }

        }

        if (!enable)
        {
            return;
        }

    }

    /*
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        //audioSource.volume = startVolume;
        float tmpVolume = startVolume;

        while (audioSource.volume > 0)
        {
            //Debug.Log(audioSource.volume);
            audioSource.volume -= tmpVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    */

    private IEnumerator SpawnAndMoveNotes()
    {
        while (true)
        {
            GameObject note;

            Quaternion orientation = gameObject.transform.rotation;
            orientation *= Quaternion.Euler(Vector3.right * 270);

            // Instantiate the prefab
            note = Instantiate(notePrefab, spawnPoint.transform.position, orientation);
            if (gameObject.CompareTag("WhiteKey"))
            {

                // Change the color of the instantiated object
                Renderer renderer = note.GetComponent<Renderer>();
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
                Renderer renderer = note.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red; // Change to the desired color
                }
                else
                {
                    UnityEngine.Debug.LogError("Renderer component not found on instantiated object.");
                }
            }
            
            // Move the note upward
            StartCoroutine(MoveUp(note));

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveUp(GameObject note)
    {
        float elapsedTime = 0f;

        while (elapsedTime < lifetime)
        {
            // Move the note upward
            //gameObject.transform.position

            note.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Destroy the note after its lifetime
        Destroy(note);
    }
}

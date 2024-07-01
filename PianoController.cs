using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : MonoBehaviour
{
    public GameObject[] pianoKeys; // Assign your piano key GameObjects in the Inspector
    public GameObject canvasGameObject;
    private Canvas canvas;
    private bool ui_enabled;

    void Start()
    {
        // Subscribe to the NumberController events
        NumberController.OnNumberChanged += UpdatePianoKeys;
        UpdatePianoKeys(87);
        canvas = canvasGameObject.GetComponent<Canvas>();
        ui_enabled = canvasGameObject.activeInHierarchy;
    }
    public void EndApplication() { Application.Quit(); }
    public void toggleUI()
    {
        canvas.enabled = !canvas.enabled;
    }
    void UpdatePianoKeys(int newNumber)
    {
        UpdateKeysAppearance(newNumber);
    }

    void UpdateKeysAppearance(int currentNumber)
    {
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            bool shouldBeActive = i <= currentNumber; 
            pianoKeys[i].SetActive(shouldBeActive);
        }
    }
}

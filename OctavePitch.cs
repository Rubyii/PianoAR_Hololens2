using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctavePitch : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Anzahl der Halbtonschritte um die Tonhöhe zu erhöhen oder zu verringern")]
    int numberOfSemitones;
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        RaisePitchBySemitone();
    }

    public void RaisePitchBySemitone()
    {
        float multiply = audioSource.pitch * Mathf.Pow(1.05946f, numberOfSemitones);

        audioSource.pitch = multiply;
    }
}

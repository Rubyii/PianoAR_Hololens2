using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Image = UnityEngine.UI.Image;

public class UIFlyingNoteOn : MonoBehaviour
{
    public GameObject[] targetObjects; // Assign the GameObjects with TargetScript in the Inspector
    public string scriptName = "ColliderPianoKeys";
    public GameObject NoteSimulator;
    public GameObject BackplateFreiesSpiel;
    public GameObject BackplateNoteSimulator;
    public PressableButton pressableButtonFreiesSpiel;
    public PressableButton pressableButtonNoteSimulator;


    //private Color blue = new Color(0.5882f, 0.8235f, 1f, 0.3921f);
    private Color green = new Color(0.5882f, 1f, 0.5882f, 0.3921f);

    private void Start()
    {
        pressableButtonNoteSimulator.OnClicked.AddListener(toggleNoteSimulator);
        pressableButtonFreiesSpiel.OnClicked.AddListener(toggleColliderPianokeys);
        BackplateNoteSimulator.GetComponent<Image>().color = !NoteSimulator.activeInHierarchy ? Color.red : green;
        BackplateFreiesSpiel.GetComponent<Image>().color = green;
    }

    public void setBackplateColor_NoteSimulator(string color)
    {
        switch (color)
        {
            case "green":
                BackplateNoteSimulator.GetComponent<Image>().color = green;
                //Debug.Log("BackplateNoteSimulator GREEN");
                //Debug.Log(BackplateNoteSimulator.GetComponent<Image>().color);
                break;
            case "red":
                BackplateNoteSimulator.GetComponent<Image>().color = Color.red;
                //Debug.Log("BackplateNoteSimulator ROT");
                //Debug.Log(BackplateNoteSimulator.GetComponent<Image>().color);
                break;
            default:
                break;
        }
    }

    public void setBackplateColor_FreiesSpiel(string color)
    {
        switch (color)
        {
            case "green":
                BackplateFreiesSpiel.GetComponent<Image>().color = green;
                //Debug.Log("BackplateFreiesSpiel ROT");
                //Debug.Log(BackplateFreiesSpiel.GetComponent<Image>().color);
                break;
            case "red":
                BackplateFreiesSpiel.GetComponent<Image>().color = Color.red;
                //Debug.Log("BackplateFreiesSpiel ROT");
                //Debug.Log(BackplateFreiesSpiel.GetComponent<Image>().color);
                break;
            default:
                break;
        }
    }

    public void toggleNoteSimulator()
    {

        Debug.Log("EINSPIELEN");
        // Toggle the target object
        if (!NoteSimulator.activeInHierarchy)
        {
            
            NoteSimulator.SetActive(true);
            //Debug.Log(NoteSimulator.activeInHierarchy);
            setBackplateColor_NoteSimulator("green");
        }
        else if (NoteSimulator.activeInHierarchy)
        {
            NoteSimulator.SetActive(false);
            //Debug.Log(NoteSimulator.activeInHierarchy);
            setBackplateColor_NoteSimulator("red");
        }

        //Disable other mode--Skript ist auf jeder Taste targetObjects hat alle Tasten
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                ColliderPianoKeys colliderPianoKeys = targetObject.GetComponent<ColliderPianoKeys>();
                colliderPianoKeys.enable = false;
            }
        }
        setBackplateColor_FreiesSpiel("red");
    }

    public void toggleColliderPianokeys()
    {
        Debug.Log("FreiesSpiel");

        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject != null)
            {
                ColliderPianoKeys colliderPianoKeys = targetObject.GetComponent<ColliderPianoKeys>();

                if (colliderPianoKeys != null)
                {
                    // Toggle the script on the target object
                    colliderPianoKeys.enable = !colliderPianoKeys.enable;
                    //Debug.Log(colliderPianoKeys.enabled);

                    if (colliderPianoKeys.enable)
                    {
                        setBackplateColor_FreiesSpiel("green");
                    }
                    else
                    {
                        setBackplateColor_FreiesSpiel("red");
                    }
                }
            }
        }

        //Disable other mode
        NoteSimulator.SetActive(false);
        setBackplateColor_NoteSimulator("red");
        
    }

    // Function to toggle the script on all target objects
    /*
    public void ToggleTargetScripts()
    {
        Debug.Log("TOGGLE");

        if (pressableButtonFreiesSpiel)
        {
            toggleNoteSimulator();
        } else if (pressableButtonNoteSimulator.IsToggled)
        {
            BackplateFreiesSpiel.GetComponent<Image>().color = new Color(151, 216, 255, 100);
            toggleColliderPianokeys();
        }
    } 
    */
}
    

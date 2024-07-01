using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumberController : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public int numberValue;

    public delegate void NumberChangedEventHandler(int newNumber);
    public static event NumberChangedEventHandler OnNumberChanged;

    private void Start()
    {
        numberValue = 87;
        UpdateNumberText();
    }

    // Called when the left button is clicked
    public void OnLeftButtonClickOneStep()
    {
        if (numberValue > 0)
        {
            numberValue--;
            UpdateNumberText();
            NotifyNumberChanged();
        }
    }

    // Called when the right button is clicked
    public void OnRightButtonClickOneStep()
    {
        if (numberValue < 87)
        {
            numberValue++;
            UpdateNumberText();
            NotifyNumberChanged();
        }
    }

    // Called when the left button is clicked
    public void OnLeftButtonClickTwelveStep()
    {
        if (numberValue >= 12)
        {
            numberValue = numberValue - 12;
            UpdateNumberText();
            NotifyNumberChanged();
        }
    }

    // Called when the right button is clicked
    public void OnRightButtonClickTwelveStep()
    {
        if (numberValue < 76)
        {
            numberValue = numberValue + 12;
            UpdateNumberText();
            NotifyNumberChanged();
        }
    }

    // Update the number text
    private void UpdateNumberText()
    {
        numberText.text = (numberValue + 1).ToString();
    }

    private void NotifyNumberChanged()
    {
        OnNumberChanged?.Invoke(numberValue);
    }
}
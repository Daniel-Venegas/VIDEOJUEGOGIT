using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class YesNoPromt : MonoBehaviour
{
    [SerializeField]
    Text promptText;
    Action onYesSelected = null;

    public void CreatePrompt(String message, Action onYesSelected)
    {
        this.onYesSelected = onYesSelected;
        promptText.text = message;
    }

    public void Answer(bool yes)
    {
        if (yes && onYesSelected != null)
        {
            onYesSelected();
        }
        onYesSelected = null ;
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirconButton : MonoBehaviour
{
    public bool ButtonUp;
    public Aircon AirconScriptRef;


    void start()
    {
        gameObject.GetComponent<Interactable>().OnInteract += ChangeScreenValue;
    }

    void ChangeScreenValue()
    {
        AirconScriptRef.value += ChangeValues[Convert.ToUInt16(ButtonUp)];
    }
    

    private static int[] ChangeValues = { 1, -1 };
}

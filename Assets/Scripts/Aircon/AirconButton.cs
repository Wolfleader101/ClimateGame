using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirconButton : MonoBehaviour
{
    public bool ButtonUp;
    public Aircon AirconScriptRef;


    void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += ChangeScreenValue;
    }

    void ChangeScreenValue(InteractableHandler handler)
    {
        AirconScriptRef.value += ChangeValues[Convert.ToUInt16(ButtonUp)];
    }
    

    private static int[] ChangeValues = { -1, 1 };
}

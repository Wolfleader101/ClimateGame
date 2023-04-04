using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AirconButton : MonoBehaviour
{
    [SerializeField] private bool buttonUp;
    [SerializeField] private Aircon airconScriptRef;


    void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += ChangeScreenValue;
    }

    void ChangeScreenValue(InteractableHandler handler)
    {
        airconScriptRef.ChangeValue(ChangeValues[Convert.ToUInt16(buttonUp)]);
    }
    

    private static int[] ChangeValues = { -1, 1 };
}

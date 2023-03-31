using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class airconButton : MonoBehaviour
{
    [SerializeField] private bool buttonUp;
    [SerializeField] private aircon airconScriptRef;


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

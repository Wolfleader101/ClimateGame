using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentHouse : MonoBehaviour
{
    [SerializeField] private NotepadScript notepad;

    [SerializeField] private PhoneScript phoneScript;
    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var house = other.GetComponent<House>();
        if (house == null) return;
        
        if (notepad != null) notepad.ActiveHouse = house;
        if (phoneScript != null) phoneScript.ActiveHouse = house;
    }

    private void OnTriggerExit(Collider other)
    {
        var house = other.GetComponent<House>();
        if (house == null) return;
        
        if (notepad != null) notepad.ActiveHouse = null;
        if (phoneScript != null) phoneScript.ActiveHouse = null;
    }
}

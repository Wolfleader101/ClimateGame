using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    //PUBLIC VARIABLES
    public DictionaryData dictionaryData;
    public bool lightOnOrOff = true; //A boolean to turn lights on or off

    //PRIVATE VARIABLES
    private GameObject switchObject;

    private const float maxLightIntensity = 0.3f;

    public void Awake()
    {
        switchObject = this.gameObject;
    }
}

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
    private const float maxEmissiveIntensity = 5.0f;
    private Color emissiveColor = new Color(0.654f, 0.749f, 0.450f);

    public void Awake()
    {
        switchObject = this.gameObject;

        if (lightOnOrOff == false)
        {
            foreach (KeyValuePair<GameObject, Light> item in dictionaryData.lightModels) //Loops through all elements within the data dictionary
            {
                item.Key.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission

                item.Value.intensity = 0.0f;
            }
        }
        else
        {
            foreach (KeyValuePair<GameObject, Light> item in dictionaryData.lightModels) //Loops through all elements within the data dictionary
            {
                item.Key.GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); //Turns on the emission

                item.Key.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * maxEmissiveIntensity); //Sets the colour and intensity

                item.Value.intensity = maxLightIntensity;
            }
        }
    }
}

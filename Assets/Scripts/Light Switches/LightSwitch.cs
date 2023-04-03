using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LightSwitch : MonoBehaviour
{
    //PUBLIC VARIABLES
    public DictionaryData lightData; //The dictionary data which holds the light and light model pair (Key is the model, value is the light)

    public bool lightOnOrOff = true; //A boolean to turn lights on or off

    //PRIVATE VARIABLES
    private const float maxLightIntensity = 0.3f; //The maximum light intensity value
    private const float maxEmissiveIntensity = 5.0f; //The maximum emission intensity value
    private const float maximumRayLength = 0.5f; //The maximum length of the ray

    private Color emissiveColour = new Color(0.654f, 0.749f, 0.450f); //The emissive colour

    private bool xrModeOn; //A boolean to check if the game is using VR or not

    private RaycastHit hitData; //A variable to hold the data from a raycast hit

    public void Awake()
    {
        if (!gameObject.GetComponent<Outline>())
        {
            gameObject.AddComponent<Outline>();
        }

        if (lightOnOrOff == false)
        {
            foreach (KeyValuePair<GameObject, Light> item in lightData.lightModels) //Loops through all elements within the data dictionary
            {
                item.Key.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission

                item.Value.intensity = 0.0f;
            }
        }
        else
        {
            foreach (KeyValuePair<GameObject, Light> item in lightData.lightModels) //Loops through all elements within the data dictionary
            {
                item.Key.GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); //Turns on the emission

                item.Key.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColour * maxEmissiveIntensity); //Sets the colour and intensity

                item.Value.intensity = maxLightIntensity;
            }
        }

        if(XRSettings.enabled) //If VR is enabled
        {
            xrModeOn = true;
        }
        else
        {
            xrModeOn = false;
        }
    }

    public void Update()
    {
        switch (xrModeOn)
        {
            case true:
                

                break;
            case false:
                if (RayToSwitch(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0))))
                {
                    ChangeLightState();
                }

                break;
        }
    }

    private bool RayToSwitch(Ray inputRay) //A function for checking if the ray is hitting the 
    {
        Ray rayToPoint = inputRay; //Updates the current ray to the new input ray

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward); //Draws a ray in the scene mode to check that it is casting

        if (Physics.Raycast(rayToPoint, out hitData, maximumRayLength) && hitData.transform.gameObject.tag == "LightSwitch")
        {
            Debug.Log("NOTICE: Light Switch being hit is '" + hitData.transform.gameObject.name + "'");

            gameObject.GetComponent<Outline>().enabled = true;

            gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
            gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;

            return true;
        }
        else
            gameObject.GetComponent<Outline>().enabled = false;

        return false;
    }

    private void ChangeLightState() //A function to swap the state of the light depending on the state of the switch
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && lightOnOrOff == false)
        {
            foreach (KeyValuePair<GameObject, Light> item in lightData.lightModels) //Loops through all elements within the data dictionary
            {
                item.Key.GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); //Turns on the emission

                item.Key.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColour * maxEmissiveIntensity); //Sets the colour and intensity

                item.Value.intensity = maxLightIntensity;
            }

            lightOnOrOff = true;
        }
        else
            if (Input.GetKeyDown(KeyCode.Mouse0) && lightOnOrOff == true)
            {
                foreach (KeyValuePair<GameObject, Light> item in lightData.lightModels) //Loops through all elements within the data dictionary
                {
                    item.Key.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission

                    item.Value.intensity = 0.0f;
                }

                lightOnOrOff = false;
            }
    }
}

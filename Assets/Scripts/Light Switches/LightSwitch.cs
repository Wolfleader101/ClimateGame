using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Outline), typeof(Interactable))]
public class LightSwitch : MonoBehaviour
{
    public bool lightOnOrOff = true; //A boolean to turn lights on or off

    //PRIVATE VARIABLES
    [SerializeField] 
    private List<Light> _lights;
    [SerializeField]
    private List<GameObject> _lightMeshes;

    private const float _maxLightIntensity = 0.3f; //The maximum light intensity value
    private const float _maxEmissiveIntensity = 0.0f; //The maximum emission intensity value
    private const float _maximumRayLength = 0.5f; //The maximum length of the ray

    private Outline _outline;

    private Color _emissiveColour = new Color(0.654f, 0.749f, 0.450f); //The emissive colour

    static private RaycastHit _hitData; //A variable to hold the data from a raycast hit

    public void Awake()
    {
        _outline = gameObject.GetComponent<Outline>();
        _outline.enabled = false;

        gameObject.GetComponent<Interactable>().OnInteractEvent += OnInteract;

        if (lightOnOrOff == false)
        {
            foreach (var item in _lights) //Loops through all elements within the data dictionary
            {
                item.intensity = 0.0f;
            }

            foreach (var item in _lightMeshes) //Loops through all elements within the data dictionary
            {
                item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            }
        }
        else
        {
            foreach (var item in _lights) //Loops through all elements within the data dictionary
            {
                item.intensity = _maxLightIntensity;
            }

            foreach (var item in _lightMeshes) //Loops through all elements within the data dictionary
            {
                item.GetComponent<Renderer>().material.SetColor("_EmissiveColor", _emissiveColour * _maxEmissiveIntensity); //Sets the colour and intensity
            }
        }
    }

    private void OnInteract(InteractableHandler handler)
    {
        if (!XRSettings.enabled && RayToSwitch(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0))))
        {
            ChangeLightState();
        }
    }

    private bool RayToSwitch(Ray inputRay) //A function for checking if the ray is hitting the 
    {
        Ray rayToPoint = inputRay; //Updates the current ray to the new input ray

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward); //Draws a ray in the scene mode to check that it is casting

        if (Physics.Raycast(rayToPoint, out _hitData, _maximumRayLength) && _hitData.transform.gameObject.tag == "LightSwitch")
        {
            Debug.Log("NOTICE: Light Switch being hit is '" + _hitData.transform.gameObject.name + "'");

            _outline.enabled = true;

            _outline.OutlineMode = Outline.Mode.OutlineVisible;
            _outline.OutlineColor = Color.yellow;
            _outline.OutlineWidth = 10.0f;

            return true;
        }
        else
            _outline.enabled = false;

        return false;
    }

    private void ChangeLightState() //A function to swap the state of the light depending on the state of the switch
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && lightOnOrOff == false)
        {
            foreach (var item in _lights) //Loops through all elements within the data dictionary
            {
                item.intensity = _maxLightIntensity;
            }

            foreach (var item in _lightMeshes) //Loops through all elements within the data dictionary
            {
                item.GetComponent<Renderer>().material.SetColor("_EmissiveColor", _emissiveColour * _maxEmissiveIntensity); //Sets the colour and intensity
            }

            lightOnOrOff = true;
        }
        else
            if (Input.GetKeyDown(KeyCode.Mouse0) && lightOnOrOff == true)
            {
                foreach (var item in _lights) //Loops through all elements within the data dictionary
                {
                    item.intensity = 0.0f;
                }

                foreach (var item in _lightMeshes) //Loops through all elements within the data dictionary
                {
                    item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                }

                lightOnOrOff = false;
            }
    }
}

<<<<<<< HEAD
=======
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Interactable))]
public class LightSwitch : MonoBehaviour
{
    [SerializeField] private bool lightOnOrOff = true; //A boolean to turn lights on or off

    [SerializeField] private List<Light> lights;

    //PRIVATE VARIABLES
    private const float MaxLightIntensity = 0.3f; //The maximum light intensity value
    private const float MaxEmissiveIntensity = 5.0f; //The maximum emission intensity value

    private readonly Color _emissiveColour = new Color(0.654f, 0.749f, 0.450f); //The emissive colour

    private Outline _outline;


    private void Start()
    {
        _outline = gameObject.GetComponent<Outline>();
        gameObject.GetComponent<Interactable>().OnInteractEvent += OnInteract;
    }

    public void Awake()
    {
        if (lightOnOrOff == false)
        {
            foreach (var l in lights)
            {
                l.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission
                
                l.intensity = 0.0f;
            }
        }
        else
        {
            foreach (var l in lights)
            {
                
                var mat = l.GetComponent<Renderer>().material;

                mat.EnableKeyword("_EMISSION"); //Turns on the emission
                mat.SetColor("_EmissiveColor", _emissiveColour * MaxEmissiveIntensity); //Sets the colour and intensity
                

                l.intensity = MaxLightIntensity;
            }
        }
    }

    private void OnInteract(InteractableHandler handler)
    {
        ChangeLightState();
    }

    private void ChangeLightState()
    {
        if (lightOnOrOff)
        {
            foreach (var l in lights)
            {
                l.GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission

                l.intensity = 0.0f;
            }

            _outline.enabled = false;
            lightOnOrOff = false;
        }
        else
        {
            foreach (var l in lights)
            {
                var mat = l.GetComponent<Renderer>().material;

                mat.EnableKeyword("_EMISSION"); //Turns on the emission
                mat.SetColor("_EmissiveColor", _emissiveColour * MaxEmissiveIntensity); //Sets the colour and intensity

                l.intensity = MaxLightIntensity;
            }

            lightOnOrOff = true;
            _outline.enabled = true;
        }
    }
}
>>>>>>> parent of ba4b2f7 (fixed minor bug)

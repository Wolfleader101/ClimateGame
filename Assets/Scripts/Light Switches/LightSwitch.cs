using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Interactable))]
public class LightSwitch : MonoBehaviour
{
    [SerializeField] private bool lightOn = true; //A boolean to turn lights on or off

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

        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.OutlineColor = Color.yellow;
        _outline.OutlineWidth = 5.0f;

        if (lightOn == false)
        {
            foreach (var l in lights)
            {
                l.GetComponentInParent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission
                
                l.intensity = 0.0f;
            }
        }
        else
        {
            foreach (var l in lights)
            {
                
                var mat = l.GetComponentInParent<Renderer>().material;

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
        if (lightOn)
        {
            foreach (var l in lights)
            {
                l.GetComponentInParent<Renderer>().material.DisableKeyword("_EMISSION"); //Turns off the emission

                l.intensity = 0.0f;
            }

            lightOn = false;
            _outline.enabled = lightOn;
        }
        else
        {
            foreach (var l in lights)
            {
                var mat = l.GetComponentInParent<Renderer>().material;

                mat.EnableKeyword("_EMISSION"); //Turns on the emission
                mat.SetColor("_EmissiveColor", _emissiveColour * MaxEmissiveIntensity); //Sets the colour and intensity

                l.intensity = MaxLightIntensity;
            }

            lightOn = true;
            _outline.enabled = lightOn;
        }
    }
}
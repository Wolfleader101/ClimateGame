using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public event Action OnInteractEvent;
    
    public void OnInteract()
    {
        Debug.Log("On Interact");
        OnInteractEvent?.Invoke();

        var outline = GetComponent<Outline>();
            if(outline != null)
            {
                outline.OutlineColor = Color.red;
            }
    }
}

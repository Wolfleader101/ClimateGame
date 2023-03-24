using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public event Action<RaycastHit, GameObject> OnInteractEvent;
    
    public void OnInteract(RaycastHit hit, GameObject interactingObj)
    {
        OnInteractEvent?.Invoke(hit, interactingObj);
    }
}

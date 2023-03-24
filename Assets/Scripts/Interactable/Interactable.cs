using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isHoldable = false;

    public bool Holdable => isHoldable;
    public event Action<InteractableHandler, RaycastHit?> OnInteractEvent;
    
    public void OnInteract(InteractableHandler handler, RaycastHit? hit)
    {
        OnInteractEvent?.Invoke(handler, hit);
    }
}

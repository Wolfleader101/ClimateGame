using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(PlayerInput))]
public class InteractableHandler : MonoBehaviour
{
    [SerializeField] private GameObject vrLeftHand;
    [SerializeField] private GameObject vrRightHand;

    private Camera _camera;

    private Interactable _heldInteractable;

    private bool _vrEnabled;

    private void Start()
    {
        _camera = Camera.main;
        _vrEnabled = XRSettings.enabled;

        vrLeftHand.SetActive(_vrEnabled);
        vrRightHand.SetActive(_vrEnabled);
    }

    public void OnVRInteract(Interactable interactable)
    {
        if (interactable.Holdable) _heldInteractable = interactable;
        interactable.OnInteract(this);
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_heldInteractable)
        {
            _heldInteractable.OnInteract(this);
            _heldInteractable = null;

            return;
        }


        // if (_vrEnabled)
        // {
        //     if (interactable.Holdable) _heldInteractable = interactable;
        //     interactable.OnInteract(this);
        //     
        //     return;
        // }

        if (_vrEnabled) return;

        var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit, 2))
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (!interactable) return;

            if (interactable.Holdable) _heldInteractable = interactable;
            interactable.OnInteract(this);
        }
    }
}
using UnityEngine;
using UnityEngine.XR;


public class InteractableHandler : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameObject vrLeftHand;
    [SerializeField] private GameObject vrRightHand;

    private Camera _camera;

    private Interactable _heldInteractable;

    private bool _vrEnabled;
    
    private bool _hasInteracted = false;

    private void Start()
    {
        _camera = Camera.main;
        _vrEnabled = XRSettings.enabled;

        vrLeftHand.SetActive(_vrEnabled);
        vrRightHand.SetActive(_vrEnabled);
    }

    private void Update()
    {
        var interact = inputHandler.Interact;
        // Only call OnInteract if it hasn't been called yet
        if(interact && !_hasInteracted) 
        {
            _hasInteracted = true;
            OnInteract();
        }else if(!interact) 
        {
            _hasInteracted = false;
        }
    }

    public void OnVRInteract(Interactable interactable)
    {
        if (interactable.Holdable) return;

        _hasInteracted = true;
        interactable.OnInteract(this);
    }
    
    private void OnInteract()
    {
        if (_hasInteracted) return;
        if (_heldInteractable)
        {
            _heldInteractable.OnInteract(this);
            _heldInteractable = null;

            return;
        }
        
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
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InteractableHandler : MonoBehaviour
{
    private Camera _camera;

    private Interactable _heldInteractable;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_heldInteractable)
        {
            _heldInteractable.OnInteract(this, null);
            _heldInteractable = null;

            return;
        }

        var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit))
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (!interactable) return;

            interactable.OnInteract(this, hit);

            if (interactable.Holdable) _heldInteractable = interactable;
        }
    }
}
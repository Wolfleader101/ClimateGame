using UnityEngine;

public class VRHandInteraction : MonoBehaviour
{
    [SerializeField] private InteractableHandler _interactableHandler;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("sadsdf");
        if (!collision.gameObject.GetComponent<Interactable>()) return;

        _interactableHandler.OnVRInteract(collision.gameObject.GetComponent<Interactable>());
    }
}
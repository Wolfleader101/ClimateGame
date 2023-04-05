using UnityEngine;

public class VRHandInteraction : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<Interactable>()) return;

        transform.parent.GetComponent<InteractableHandler>().OnVRInteract(collision.gameObject.GetComponent<Interactable>());
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System.Collections.Generic;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset VRInput;
    private InputAction input;

    private List<Image> images;

    [SerializeField] private Quaternion rotation;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Image>(out Image image))
            {
                images.Add(image);
            }
        }

        input = VRInput.FindActionMap("XRI LeftHand").FindAction("Rotation");
    }

    private void Update()
    {
        rotation = input.ReadValue<Quaternion>();

        print(rotation);
    }
}

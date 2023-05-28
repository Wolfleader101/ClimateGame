using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System.Collections.Generic;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private Transform LH_controller;

    [SerializeField] private List<Image> images;

    [SerializeField] private Vector3 eulerRotation;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Image>(out Image image))
            {
                images.Add(image);
            }
        }
    }

    private void Update()
    {
        eulerRotation = LH_controller.localEulerAngles;

        print(eulerRotation);
    }
}

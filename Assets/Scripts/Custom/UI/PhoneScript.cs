using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System.Collections.Generic;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private Transform LH_controller;

    [SerializeField] private List<Image> images;

    [SerializeField] private Vector3 eulerRotation;

    [SerializeField] private float minZ = -160.0f;
    [SerializeField] private float maxZ = -70.0f;

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

        if (eulerRotation.z >= minZ && eulerRotation.z <= maxZ)
        {
            SetColor(false);
            print("good rotation");
        }
        else
        {
            SetColor(true);

        }
    }

    private void SetColor(bool clear)
    {
        for (int i = 0; i < images.Count; ++i)
        {
            if (clear)
                images[i].color = Color.Lerp(Color.white, Color.clear, Time.deltaTime);
            else
                images[i].color = Color.Lerp(Color.clear, Color.white, Time.deltaTime);
        }
    }
}

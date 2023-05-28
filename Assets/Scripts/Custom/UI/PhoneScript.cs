using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private Transform LH_controller;
    [SerializeField] private Vector3 eulerRotation;

    [SerializeField] private Canvas canvas;

    [SerializeField] private float minZ = 0.0f;
    [SerializeField] private float maxZ = 0.0f;

    private void Start()
    {
    }

    private void Update()
    {
        eulerRotation = LH_controller.localEulerAngles;

        if (eulerRotation.z <= minZ && eulerRotation.z >= maxZ)
            canvas.gameObject.SetActive(true);
        else
            canvas.gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private InputHandler handler;

    [SerializeField] private List<GameObject> children;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            print(child.name);
        }
    }


}

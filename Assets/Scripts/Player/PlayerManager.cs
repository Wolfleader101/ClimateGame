using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject nonVrPlayer;
    [SerializeField] private GameObject vrPlayer;
    [SerializeField] private InputHandler inputHandler;

    void Start()
    {
        Invoke("CheckForVR", 5f);
    }

    void CheckForVR()
    {
        if (inputHandler.VrEnabled)
        {
            nonVrPlayer.SetActive(false);
            vrPlayer.SetActive(true);
        }
        else
        {
            nonVrPlayer.SetActive(true);
            vrPlayer.SetActive(false);
        }
    }
}

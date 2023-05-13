using System;
using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using TrackedPoseDriver = UnityEngine.SpatialTracking.TrackedPoseDriver;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    
    public bool Jump { get; private set; }
    
    public bool Interact { get; private set; }
    
    public bool VrEnabled { get; private set; }

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;
    private InputAction _jumpAction;
    
    private void Awake()
    {
        _moveAction = playerInput.actions["Move"];
        _lookAction = playerInput.actions["Look"];
        _interactAction = playerInput.actions["Interact"];
        _jumpAction = playerInput.actions["Jump"];
        Cursor.lockState = CursorLockMode.Locked;
        VrEnabled = XRSettings.enabled && XRSettings.isDeviceActive;
    }



    // Update is called once per frame
    void Update()
    {
        VrEnabled = XRSettings.enabled && XRSettings.isDeviceActive;
        Move = _moveAction.ReadValue<Vector2>();
        Look = _lookAction.ReadValue<Vector2>();
        Jump = _jumpAction.ReadValue<float>() > 0.0f;
        Interact = _interactAction.ReadValue<float>() > 0.0f;
    }
}
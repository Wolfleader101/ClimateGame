using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool useVr;

    [SerializeField] private float mouseSens = 10f;
    
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravityScale = -9.81f;
    [SerializeField] private BaseCharacter character;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private float _xPos;
    private float _zPos;


    private Vector3 _velocity;
    
    private Camera _camera;
    
    private void Start()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();

        //lock cursor to mid
        Cursor.lockState = CursorLockMode.Locked;
        
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _xRotation -= _mouseY;
        _yRotation -= -_mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // fix rotation LMAO
        // ez fix
        // just rotate torso and head NOT whole game object
        transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
        cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        Vector3 motion = transform.right * _xPos + transform.forward * _zPos;
        characterController.Move(motion * (character.MovementSpeed * Time.deltaTime));

        _velocity.y += (character.GravityScaleMultiplier * gravityScale) * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
    }
    
    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        _xPos = direction.x;
        _zPos = direction.y;
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        var sens = mouseSens;
        if(value.control.name == "trackpad")
        {
            sens = 100;
        }
        Vector2 direction = value.ReadValue<Vector2>();
        _mouseX = direction.x * sens * Time.deltaTime;
        _mouseY = direction.y * sens * Time.deltaTime;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (character.CanDoubleJump && value.performed && value.interaction is MultiTapInteraction)
        {
            _velocity.y = Mathf.Sqrt(character.JumpHeight * -2f * gravityScale);
            Debug.Log("Double Jump");
        }

        if (characterController.isGrounded && value.started)
        {
            _velocity.y = Mathf.Sqrt(character.JumpHeight * -2f * gravityScale);
        }
    }
}

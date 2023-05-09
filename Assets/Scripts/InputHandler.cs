using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private BaseCharacter character;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject cam;
    [SerializeField] private float speedChangeRate = 10.0f;


    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;


    private float _xRotation = 0;
    private float _yRotation = 0;

    private float _speed;
    private float _verticalVelocity;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
				return playerInput.currentControlScheme == "KeyboardMouse";
#else
            return false;
#endif
        }
    }
    
    private bool IsCurrentXR
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return playerInput.currentControlScheme == "XR";
#else
            return false;
#endif
        }
    }

    private void Awake()
    {
        _moveAction = playerInput.actions["Move"];
        _lookAction = playerInput.actions["Look"];
        _interactAction = playerInput.actions["Interact"];
        Cursor.lockState = CursorLockMode.Locked;

        if (!controller) controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        // ground check

        Move();

        Look();
    }

    private void Jump()
    {
        // _velocity.y += (character.GravityScaleMultiplier * gravityScale) * Time.deltaTime;
        // characterController.Move(_velocity * Time.deltaTime);
    }

    private void Move()
    {
        var move = _moveAction.ReadValue<Vector2>();
        

        float targetSpeed = character.MovementSpeed;

        if (move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (move != Vector2.zero)
        {
            // move
            inputDirection = transform.right * move.x + transform.forward * move.y;
        }

        // move the player
        controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) +
                        new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void Look()
    {
        var look = _lookAction.ReadValue<Vector2>();


        if (look.sqrMagnitude >= 0.01f)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            
            _xRotation += look.y * rotationSpeed * deltaTimeMultiplier;
            _yRotation = look.x * rotationSpeed * deltaTimeMultiplier;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(_xRotation, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _yRotation);
        }
    }
}
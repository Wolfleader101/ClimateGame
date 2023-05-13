using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private BaseCharacter character;

    [SerializeField] private float mouseSens = 1.0f;
    [SerializeField] private float gravityScale = -9.81f;


    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private float _verticalVelocity = -2;
    private float _terminalVelocity = 53.0f;


    private void Start()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();

        //lock cursor to mid
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        Move();
    }

    private void LateUpdate()
    {
        Look();
    }


    private void Jump()
    {
        var jump = inputHandler.Jump;

        if (characterController.isGrounded)
        {
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (jump)
            {
                _verticalVelocity =
                    Mathf.Sqrt(character.JumpHeight * -2f * (character.GravityScaleMultiplier * gravityScale));
            }
        }


        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravityScale * Time.deltaTime;
        }

        characterController.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        
    }

    private void Move()
    {
        var move = inputHandler.Move;
        var targetSpeed = character.MovementSpeed;

        if (move == Vector2.zero) return;

        Vector3 inputDirection = transform.right * move.x + transform.forward * move.y;

        // move the player
        characterController.Move(inputDirection * (targetSpeed * Time.deltaTime));
    }

    private void Look()
    {
        var look = inputHandler.Look;

        if (look.sqrMagnitude >= 0.01f)
        {
            _xRotation += look.y * mouseSens;
            _yRotation = look.x * mouseSens;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);


            // rotate camera up and down
            var prevCam = cam.transform.localEulerAngles;
            cam.transform.localEulerAngles =
                new Vector3(_xRotation, prevCam.y, prevCam.z);


            // rotate the player left and right
            transform.Rotate(Vector3.up * _yRotation);
        }
    }
}
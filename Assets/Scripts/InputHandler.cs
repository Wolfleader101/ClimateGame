using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private BaseCharacter character;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject cam;


    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _interactAction;


    private float _xRotation = 0;
    private float _yRotation = 0;

    private float _speed;
    private float _verticalVelocity;

    private bool _vrEnabled = false;
    
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
        _vrEnabled = XRSettings.enabled && XRSettings.isDeviceActive;
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


        _speed = targetSpeed;
        
        // normalise input direction
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

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
            float deltaTimeMultiplier = _vrEnabled ? 5.0f : 1.0f;
            
            _xRotation += look.y * rotationSpeed * deltaTimeMultiplier;
            _yRotation = look.x * rotationSpeed * deltaTimeMultiplier;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(_xRotation, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _yRotation);
        }

        if (!_vrEnabled) return;
        var prev = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(prev.x, cam.transform.localRotation.eulerAngles.y, prev.z);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Interactable))]
public class RecyclableRubbish : MonoBehaviour
{
    [Header("Joint Configuration")]
    public float force = 115.0f;
    public float damping = 7.5f;

    [Range(0, 2.0f)][SerializeField] private float grabDistance = 1.0f;

    
    private Camera _camera;
    private Rigidbody _rb;

    private GameObject _joint;
    
    private Vector3 _currentPos;
    
    private bool _held;

    
    private void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += OnInteract;

        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (!collision.gameObject.GetComponent<Recyclebin>()) return;
        
        Destroy(gameObject);
        
        collision.gameObject.GetComponent<Recyclebin>().IncrementScore();
    }
    
    private void Update()
    {
       _currentPos = _held ? _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, grabDistance)) : Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        if (_joint)
            _joint.transform.position = _currentPos;
    }

    private void OnInteract(InteractableHandler handler)
    {
        _held = !_held;
        if (!_held && _joint)
        {
            Destroy(_joint);
            return;
        }

        _joint = CreateJoint(transform.position, _rb);
    }
    
    private GameObject CreateJoint(Vector3 attachedPoint, Rigidbody rb)
    {
        var go = new GameObject("Attached Joint")
        {
            transform =
            {
                position = attachedPoint
            }
        };
        go.transform.SetParent(gameObject.transform);

        var nrb = go.AddComponent<Rigidbody>();
        nrb.isKinematic = true;

        var joint = go.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rb;
        joint.configuredInWorldSpace = true;

        joint.xDrive = CreateJointDrive();
        joint.yDrive = CreateJointDrive();
        joint.zDrive = CreateJointDrive();

        joint.slerpDrive = CreateJointDrive();

        joint.rotationDriveMode = RotationDriveMode.Slerp;

        return go;
    }

    private JointDrive CreateJointDrive()
    {
        var drive = new JointDrive
        {
            positionSpring = force,
            positionDamper = damping,
            maximumForce = Mathf.Infinity
        };

        return drive;
    }
    

}
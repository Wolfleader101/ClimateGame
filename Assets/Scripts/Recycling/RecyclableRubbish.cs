using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Interactable))]
public class RecyclableRubbish : MonoBehaviour
{
    [Header("Joint Configuration")]
    public float force = 100.0f;
    public float damping = 30.0f;

    [Range(0, 10)] [SerializeField] private float grabDistance = 10f;

    private Camera _camera;
    private Rigidbody _grabbedBody;
    private Transform _grabbedTransform;
    
    private Vector3 _currentPos;
    
    private bool _held;
    
    private void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += OnInteract;

        _camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("RecycleBin") || !other.GetComponent<Recyclebin>()) return;
        
        Destroy(gameObject);
        other.GetComponent<Recyclebin>().IncrementScore();
    }
    
    private void Update()
    {
        _grabbedBody = !_held && _grabbedBody ? null : _grabbedBody;

        var canDrag = _held && _grabbedBody;

       var dragPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, grabDistance));

       _currentPos = _held && canDrag ? dragPosition : Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        if (_grabbedTransform)
            _grabbedTransform.position = _currentPos;
    }

    private void OnInteract(RaycastHit hit, GameObject interactingObj)
    {
        _held = !_held;
        if (!_held && _grabbedTransform.gameObject)
        {
            Debug.Log("DROPPED");
            Destroy(_grabbedTransform.gameObject);
        }
        
        _grabbedBody = hit.rigidbody;
        grabDistance = hit.distance;

        _grabbedTransform = CreateJoint(hit.point, _grabbedBody);
    }
    
    private Transform CreateJoint(Vector3 attachedPoint, Rigidbody rb)
    {
        var go = new GameObject("Attached Joint")
        {
            transform =
            {
                position = attachedPoint
            }
        };

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

        return go.transform;
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
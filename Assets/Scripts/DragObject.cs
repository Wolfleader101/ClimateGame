using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [Range(0, 100)] public float maxDistance;
    public LayerMask mask;

    [Space]

    [Header("Joint Configuration")]
    public float force = 1.0f;
    public float damping = 1.0f;

    [SerializeField] private Rigidbody grabbedBody;
    [SerializeField] private Transform grabbedJoint;

    [Space]

    private bool clicked;
    [SerializeField] private bool held;
    [SerializeField] private bool canDrag;

    [Space]

    [SerializeField] private float grabDistance;

    [Space]
    
    private Vector3 dragPosition;

    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 current;

    void Update()
    {
        clicked = Input.GetMouseButtonDown(0);
        held = Input.GetMouseButton(0);

        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, mask))
            {
                start = hit.point;
            
                grabbedBody = hit.rigidbody;
                grabDistance = hit.distance;

                grabbedJoint = CreateJoint(start, grabbedBody);
            }
        }

        grabbedBody = !held && grabbedBody ? null : grabbedBody;

        canDrag = held && grabbedBody;

        dragPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, grabDistance));

        current = held && canDrag ? dragPosition : Vector3.zero;

        if (!held && grabbedJoint)
            Destroy(grabbedJoint.gameObject);
    }

    void FixedUpdate()
    {
        if (grabbedJoint != null)
            grabbedJoint.position = current;
    }

    private Transform CreateJoint(Vector3 attachedPoint, Rigidbody rb)
    {
        var go = new GameObject("Attached Joint");
        go.transform.position = attachedPoint;

        var nrb = go.AddComponent<Rigidbody>();
        nrb.isKinematic = true;

        var joint = go.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rb;
        joint.configuredInWorldSpace = true;

        joint.xDrive = CreateJointDrive(force, damping);
        joint.yDrive = CreateJointDrive(force, damping);
        joint.zDrive = CreateJointDrive(force, damping);

        joint.slerpDrive = CreateJointDrive(force, damping);

        joint.rotationDriveMode = RotationDriveMode.Slerp;

        return go.transform;
    }

    private JointDrive CreateJointDrive(float force, float damping)
    {
        JointDrive drive = new JointDrive();

        drive.positionSpring = force;
        drive.positionDamper = damping;
        drive.maximumForce = Mathf.Infinity;

        return drive;
    }
}

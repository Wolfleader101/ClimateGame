using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCam : MonoBehaviour
{
    public Transform target;

    [Space]

    [SerializeField] private float smoothTime = 0.3f;

    [Space]

    [SerializeField] private Vector3 forceOffset;
    
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offset = forceOffset + (transform.position - target.position);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.LookAt(target);
    }
}

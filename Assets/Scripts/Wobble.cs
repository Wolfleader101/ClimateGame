using UnityEngine;

public class Wobble : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float wobbleSpeed = 1.0f;
    
    [SerializeField, Range(0.0f, 1.0f)]
    private float recovery = 1.0f;

    [SerializeField] private Vector3 wobbleShift;
    
    [SerializeField, Range(0.0f, 0.1f)] 
    private float maxWobble = 0.03f;

    [Space]

    private Vector3 wobbleAmount;
    private Vector3 wobbleShiftAmount;

    [Space]

    private Vector3 velocity;
    private Vector3 angularVelocity;

    [Space]

    private Vector3 lastPos;
    private Vector3 lastRot;

    [Space]

    private float pulse;
    private float time = 0.5f;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;

        wobbleShiftAmount = wobbleShift;
    }

    private void Update()
    {
        time += Time.deltaTime;

        wobbleShiftAmount.x = Mathf.Lerp(wobbleShiftAmount.x, 0, Time.deltaTime * recovery);
        wobbleShiftAmount.z = Mathf.Lerp(wobbleShiftAmount.z, 0, Time.deltaTime * recovery);

        pulse = 2 * Mathf.PI * wobbleSpeed;

        wobbleAmount.x = wobbleShiftAmount.x * Mathf.Sin(pulse * time);
        wobbleAmount.z = wobbleShiftAmount.z * Mathf.Sin(pulse * time);

        material.SetVector("_Wobble", wobbleAmount);



        wobbleShiftAmount.x += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        wobbleShiftAmount.z += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        lastPos = transform.position;
        lastRot = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.eulerAngles - lastRot;
    }
}

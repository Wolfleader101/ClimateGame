using UnityEngine;

[ExecuteInEditMode]
public class LiquidScript : MonoBehaviour
{
    [Header("Wobble")]
    [SerializeField] private float maxWobble = 0.03f;
    [SerializeField] private float wobbleSpeed = 1.0f;

    [Space]

    [SerializeField, Range(0.0f, 1.0f)] private float fillAmount = 0.50f;
    [SerializeField] private float recovery = 1.0f;
    [SerializeField] private float thickness = 1.0f;

    [SerializeField, Range(0.0f, 1.0f)] private float shapeCompensation;


    private float wobbleX, wobbleZ;
    private float wobbleAddX, wobbleAddZ;

    private float pulse;
    private float wave;
    private float time = 0.5f;

    private Vector3 lastPos, pos;
    private Quaternion lastRot;

    private Vector3 angularVelocity, velocity;

    private Vector3 comp;

    private Material mat;
    private Mesh mesh;

    private void Awake()
    {
        GetMeshAndMaterial();
    }

    private void OnValidate()
    {
        GetMeshAndMaterial();
    }

    private void GetMeshAndMaterial()
    {
        if (mesh == null)
            mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mat == null)
        {
            foreach (Material material in GetComponent<MeshRenderer>().sharedMaterials)
                if (material.name == "Liquid")
                    mat = material;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (Time.deltaTime != 0.0f)
        {
            wobbleAddX = Mathf.Lerp(wobbleAddX, 0.0f, Time.deltaTime * recovery);
            wobbleAddZ = Mathf.Lerp(wobbleAddZ, 0.0f, Time.deltaTime * recovery);

            pulse = 2 * Mathf.PI * wobbleSpeed;
            wave = Mathf.Lerp(wave, Mathf.Sin(pulse * time), Time.deltaTime * Mathf.Clamp(velocity.magnitude + angularVelocity.magnitude, thickness, 10.0f));

            wobbleX = wobbleAddX * wave;
            wobbleZ = wobbleAddZ * wave;

            velocity = (lastPos - transform.position) / Time.deltaTime;
            angularVelocity = GetAngularVelocity(lastRot, transform.rotation);

            wobbleAddX += Mathf.Clamp((velocity.x + (velocity.y * 0.2f) + angularVelocity.z + angularVelocity.y) * maxWobble, -maxWobble, maxWobble);
            wobbleAddZ += Mathf.Clamp((velocity.z + (velocity.y * 0.2f) + angularVelocity.x + angularVelocity.y) * maxWobble, -maxWobble, maxWobble);
        }

        mat.SetFloat("_WobbleX", wobbleX);
        mat.SetFloat("_WobbleZ", wobbleZ);

        UpdatePosition();

        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    private void UpdatePosition()
    {
        Vector3 fillMin = mesh.bounds.min;
        Vector3 fillMax = mesh.bounds.max;

        if (shapeCompensation > 0.0f)
            pos = new Vector3(0.0f, Mathf.Lerp(fillMin.y - 0.5f, 0.0f + shapeCompensation, fillAmount), 0.0f);
        else
            pos = new Vector3(0.0f, Mathf.Lerp(fillMin.y - 0.5f, 0.0f, fillAmount), 0.0f);

        mat.SetVector("_FillAmount", pos);
    }

    Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation)
    {
        var q = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);
        // no rotation?
        // You may want to increase this closer to 1 if you want to handle very small rotations.
        // Beware, if it is too close to one your answer will be Nan
        if (Mathf.Abs(q.w) > 1023.5f / 1024.0f)
            return Vector3.zero;
        float gain;
        // handle negatives, we could just flip it but this is faster
        if (q.w < 0.0f)
        {
            var angle = Mathf.Acos(-q.w);
            gain = -2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }
        else
        {
            var angle = Mathf.Acos(q.w);
            gain = 2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }
        Vector3 angularVelocity = new Vector3(q.x * gain, q.y * gain, q.z * gain);
 
        if (float.IsNaN(angularVelocity.z))
        {
            angularVelocity = Vector3.zero;
        }
        return angularVelocity;
    }
}

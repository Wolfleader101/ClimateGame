using UnityEngine;

[ExecuteInEditMode]
public class LiquidScript : MonoBehaviour
{
    [Header("Wobble")]
    [SerializeField] private float maxWobble = 0.03f;
    [SerializeField] private float wobbleSpeed = 1.0f;

    [Space]

    [SerializeField] private float fillAmount = 1.0f;
    [SerializeField] private float recovery = 1.0f;
    [SerializeField] private float thickness = 1.0f;

    [SerializeField, Range(0.0f, 1.0f)] private float shapeCompensation;


    private float wobbleX, wobbleZ;
    private float wobbleAddX, wobbleAddZ;

    private float pulse;
    private float wave;
    private float time = 0.5f;

    private Vector3 pos;

    private Vector3 comp;

    private Material mat;
    private Rigidbody rb;
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
            mat = GetComponent<MeshRenderer>().sharedMaterial;
        if (rb == null)
        {
            rb = GetComponentInParent<Rigidbody>();

            if (rb == null)
                rb = GetComponent<Rigidbody>();
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
            wave = Mathf.Lerp(wave, Mathf.Sin(pulse * time), Time.deltaTime * Mathf.Clamp(rb.velocity.sqrMagnitude + rb.angularVelocity.sqrMagnitude, thickness, 10.0f));

            wobbleX = wobbleAddX * wave;
            wobbleZ = wobbleAddZ * wave;

            wobbleAddX += Mathf.Clamp((rb.velocity.x + (rb.velocity.y * 0.2f) + rb.angularVelocity.z + rb.angularVelocity.y) * maxWobble, -maxWobble, maxWobble);
            wobbleAddZ += Mathf.Clamp((rb.velocity.z + (rb.velocity.y * 0.2f) + rb.angularVelocity.x + rb.angularVelocity.y) * maxWobble, -maxWobble, maxWobble);
        }

        mat.SetFloat("_WobbleX", wobbleX);
        mat.SetFloat("_WobbleZ", wobbleZ);

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 worldPos = transform.TransformPoint(mesh.bounds.center);

        if (shapeCompensation > 0.0f)
        {
            if (Time.deltaTime != 0.0f)
                comp = Vector3.Lerp(comp, (worldPos - new Vector3(0, GetLowestPointOnMesh(), 0.0f)), Time.deltaTime * 10.0f);
            else
                comp = worldPos - new Vector3(0.0f, GetLowestPointOnMesh(), 0.0f);

            pos = worldPos - transform.position - new Vector3(0.0f, fillAmount - (comp.y * shapeCompensation), 0.0f);
        }
        else
            pos = new Vector3(0.0f, Mathf.Lerp(-1.5f, 0.5f, fillAmount), 0.0f);

        mat.SetVector("_FillAmount", pos);
    }

    private float GetLowestPointOnMesh()
    {
        float lowestY = float.MaxValue;
        Vector3 lowestVert = Vector3.zero;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; ++i)
        {
            Vector3 position = transform.TransformPoint(vertices[i]);

            if (position.y < lowestY)
            {
                lowestY = position.y;
                lowestVert = position;
            }
        }

        return lowestVert.y;
    }
}

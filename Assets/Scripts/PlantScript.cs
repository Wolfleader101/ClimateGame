using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    private float dirtAmount = 0.0f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float waterAmount = 0.0f;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Grow()
    {
        
    }

    public void Fill()
    {
        dirtAmount = 1.0f;
    }

    private void Update()
    {

    }
}

using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public static EventManager.OnValueChangeDelegate OnValueChange;

    [SerializeField, Range(0, 100)]
    private int dirtFillPercent = 0;

    private float dirtFill = 0.0f;

    private Material material;

    public float DirtFill
    {
        get { return dirtFill; }
        set
        {
            if (dirtFill == value) return;
            
            dirtFill = value;
            
            if (OnValueChange != null)
                OnValueChange(dirtFill);
        }
    }

    private void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;

        OnValueChange += HandleChange;
    }

    private void HandleChange(float val)
    {
        print("Value Changed!");

        material.SetFloat("_Fill", DirtFill);
    }

    private void Grow()
    {
        
    }

    private void Update()
    {
        DirtFill = (float)dirtFillPercent * 0.01f;
    }
}

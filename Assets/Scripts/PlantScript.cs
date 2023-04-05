using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Interactable))]
public class PlantScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float dirtFill = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float plantFill = 0.0f;

    [SerializeField] private Material dirtMaterial;
    [SerializeField] private Material plantMaterial;

    [SerializeField] private bool canGrow = false; // show for debug (hide in inspector)

    private void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += FillDirt;

        dirtMaterial.SetFloat("_FillAmount", 0.0f);
        plantMaterial.SetFloat("_FillAmount", 0.0f);
    }
    private void FillDirt(InteractableHandler handler)
    {
        dirtFill += 0.1f;
        Fill(dirtFill, dirtMaterial);

        if (dirtFill > 0.94f)
            canGrow = true;
        else StopAllCoroutines();
    }

    private void Update()
    {
        if (canGrow && plantFill < 0.96f)
            StartCoroutine(GrowPlant());
    }

    private IEnumerator GrowPlant()
    {   
        plantFill = 0.05f * Time.time;
        Fill(plantFill, plantMaterial);

        yield return null;
    }


    private void Fill(float val, Material material)
    {
        material.SetFloat("_FillAmount", val);
    }
}

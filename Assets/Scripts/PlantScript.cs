using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Interactable))]
public class PlantScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float dirtFill = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float plantFill = 0.0f;

    [SerializeField] private Material dirtMaterial;
    [SerializeField] private Material plantMaterial;

    [SerializeField] private bool usingDirt = false;
    
    public event Action OnGrown;
    
    private bool canGrow = false; // show for debug (hide in inspector)

    private float timer = 0.0f;

    private bool _grown;

    private void Start()
    {
        gameObject.GetComponent<Interactable>().OnInteractEvent += Interact;

        if (usingDirt)
            dirtMaterial.SetFloat("_FillAmount", 0.0f);

        plantMaterial.SetFloat("_FillAmount", 0.0f);
    }
    private void Interact(InteractableHandler handler)
    {
        if (usingDirt)
        {
            dirtFill += 0.1f;
            Fill(dirtFill, dirtMaterial);

            if (dirtFill > 0.94f)
                canGrow = true;
            else StopAllCoroutines();
        }
        else
            canGrow = true;
        
        if (canGrow && !_grown)
        {
            OnGrown();
        }
        
        if (dirtFill > 1.0f) _grown = true;
    }

    private void Update()
    {
        if (canGrow && plantFill < 0.96f)
            StartCoroutine(GrowPlant());

    }

    private IEnumerator GrowPlant()
    {   
        timer += Time.deltaTime;

        plantFill = 0.20f * timer;
        Fill(plantFill, plantMaterial);


        yield return null;
    }


    private void Fill(float val, Material material)
    {
        material.SetFloat("_FillAmount", val);
    }
}

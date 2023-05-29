using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private List<Interactable> _houseInteractables = new List<Interactable>();

    public List<Interactable> HouseInteractables => _houseInteractables;
    
    // Start is called before the first frame update
    void Awake()
    {
        var interactablesParent = transform.Find("Interactables");

        AddInteractablesFromTransform(interactablesParent != null ? interactablesParent : transform);
    }

    private void AddInteractablesFromTransform(Transform trans)
    {
        foreach (Transform child in trans)
        {
            var interactable = child.GetComponent<Interactable>();
            if (interactable == null) continue;

            _houseInteractables.Add(interactable);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

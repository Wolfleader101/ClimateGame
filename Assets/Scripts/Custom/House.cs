using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    
    private List<Interactable> _houseInteractables = new List<Interactable>();

    public List<Interactable> HouseInteractables => _houseInteractables;
    
    
    
    // keep track of lights
    private List<LightSwitch> _switches;
    private int _lightsOff;

    // keep track of aircon
    private List<Aircon> _aircons;
    private int _airconTemp;


    // keep track of pot plant
    private List<PlantScript> _plants;
    private int _plantsGrown;

    // keep track of recycle bins
    private List<Recyclebin> _bins;
    private int _rubbishCollected;
    private int _totalRubbish;

    public int LightsOff => _lightsOff;
    public int TotalSwitches => _switches.Count();
    public int AirconTemp => _airconTemp;
    public int TotalPlants => _plants.Count();
    public int PlantsGrown => _plantsGrown;
    public int RubbishCollected => _rubbishCollected;
    public int TotalRubbish => _totalRubbish;
    
    public bool AirconTargetHit { get; set; }
    
    

    
    // Start is called before the first frame update
    void Awake()
    {
        // var interactablesParent = transform.Find("Interactables");

        AddInteractablesFromTransform(transform);
        
        _bins = _houseInteractables
            .Select(interactable => interactable.GetComponent<Recyclebin>())
            .Where(bin => bin != null)
            .ToList();
        
        _bins.ForEach(bin => bin.OnRubbishIncrement += OnBinScoreIncrease);

        _totalRubbish = _houseInteractables
            .Select(interactable => interactable.GetComponent<RecyclableRubbish>()).Count(rubbish => rubbish != null);
        
        _aircons = _houseInteractables
            .Select(interactable => interactable.GetComponent<Aircon>())
            .Where(ac => ac != null)
            .ToList();
            
        _aircons.ForEach(ac =>
        {
            ac.OnAirconValueChange += OnAirconChange;
            _airconTemp = ac.Temp;
            AirconTargetHit = ac.TargetHit;
        });

        _switches = _houseInteractables
            .Select(interactable => interactable.GetComponent<LightSwitch>())
            .Where(ls => ls != null)
            .ToList();


        _plants = _houseInteractables
            .Select(interactable => interactable.GetComponent<PlantScript>())
            .Where(p => p != null)
            .ToList();
        
        _plants.ForEach(plant => plant.OnGrown += OnGrow);


        _collider = gameObject.GetComponent<Collider>();
    }

    private void AddInteractablesFromTransform(Transform trans)
    {
        foreach (Transform child in trans)
        {
            var interactable = child.GetComponent<Interactable>();
            if (interactable != null)
            {
                _houseInteractables.Add(interactable);
            }
            // If this child has its own children, recursively call this function
            if (child.childCount > 0)
            {
                AddInteractablesFromTransform(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _lightsOff = 0;

        foreach (var sw in _switches)
        {
            _lightsOff += sw.LightOn ? 0 : 1;
        }
        
        _aircons.ForEach(ac =>
        {
            AirconTargetHit = ac.TargetHit;
        });

        
    }

    void OnBinScoreIncrease()
    {
        _rubbishCollected++;
    }

    void OnAirconChange(int value)
    {
        _airconTemp = value;
    }

    void OnGrow()
    {
        _plantsGrown++;
    }
}

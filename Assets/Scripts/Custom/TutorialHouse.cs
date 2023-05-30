using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(House))]
public class TutorialHouse : MonoBehaviour
{ 
    [SerializeField] private House house;

    [SerializeField] private TextMeshProUGUI lightUI;
    [SerializeField] private TextMeshProUGUI plantUI;
    [SerializeField] private TextMeshProUGUI airconUI;
    [SerializeField] private TextMeshProUGUI binUI;
    
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



    private void Start()
    {
        _bins = house.HouseInteractables
            .Select(interactable => interactable.GetComponent<Recyclebin>())
            .Where(bin => bin != null)
            .ToList();
        
        _bins.ForEach(bin => bin.OnRubbishIncrement += OnBinScoreIncrease);

        _totalRubbish = house.HouseInteractables
            .Select(interactable => interactable.GetComponent<RecyclableRubbish>()).Count(rubbish => rubbish != null);
        
        _aircons = house.HouseInteractables
            .Select(interactable => interactable.GetComponent<Aircon>())
            .Where(ac => ac != null)
            .ToList();
            
        _aircons.ForEach(ac =>
        {
            ac.OnAirconValueChange += OnAirconChange;
            _airconTemp = ac.Temp;
        });

        _switches = house.HouseInteractables
            .Select(interactable => interactable.GetComponent<LightSwitch>())
            .Where(ls => ls != null)
            .ToList();


        _plants = house.HouseInteractables
            .Select(interactable => interactable.GetComponent<PlantScript>())
            .Where(p => p != null)
            .ToList();
        
        _plants.ForEach(plant => plant.OnGrown += OnGrow);
    }

    // Update is called once per frame
    void Update()
    {
        if(airconUI)
            airconUI.text = _airconTemp + "°C";
        
        if(binUI)
            binUI.text = _rubbishCollected + "/" + _totalRubbish;
        
        if(plantUI)
            plantUI.text = _plantsGrown + "/" + _plants.Count;
        
        foreach (var sw in _switches)
        {
            _lightsOff += sw.LightOn ? 0 : 1;
        }
        
        if(lightUI)
            lightUI.text = _lightsOff + "/" + _switches.Count;
        
        _lightsOff = 0;
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

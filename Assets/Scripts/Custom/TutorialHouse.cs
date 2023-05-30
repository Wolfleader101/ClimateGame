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
    
    // Update is called once per frame
    void Update()
    {
        if(airconUI)
            airconUI.text = house.AirconTemp + "°C";
        
        if(binUI)
            binUI.text = house.RubbishCollected + "/" + house.TotalRubbish;
        
        if(plantUI)
            plantUI.text = house.PlantsGrown + "/" + house.TotalPlants;
        
        if(lightUI)
            lightUI.text = house.LightsOff + "/" + house.TotalSwitches;
        
    }


}

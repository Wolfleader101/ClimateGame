using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
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



    private void Awake()
    {
        _bins = new List<Recyclebin>(Object.FindObjectsOfType<Recyclebin>());
        foreach (var bin in _bins)
        {
            bin.OnRubbishIncrement += OnBinScoreIncrease;
        }

        _totalRubbish = Object.FindObjectsOfType<RecyclableRubbish>().Length;

        _aircons = new List<Aircon>(Object.FindObjectsOfType<Aircon>());
        foreach (var ac in _aircons)
        {
            ac.OnAirconValueChange += OnAirconChange;
            _airconTemp = ac.Temp;
        }

        _switches = new List<LightSwitch>(Object.FindObjectsOfType<LightSwitch>());

        _plants = new List<PlantScript>(Object.FindObjectsOfType<PlantScript>());
        foreach (var plant in _plants)
        {
            plant.OnGrown += OnGrow;
        }

    }

    // Update is called once per frame
    void Update()
    {
        airconUI.text = _airconTemp + "°C";
        binUI.text = _rubbishCollected + "/" + _totalRubbish;
        plantUI.text = _plantsGrown + "/" + _plants.Count;
        
        foreach (var sw in _switches)
        {
            _lightsOff += sw.LightOn ? 0 : 1;
        }
        
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


    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}

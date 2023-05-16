using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Aircon : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private int _value;

    public int Temp => _value;
    
    public event Action<int> OnAirconValueChange;
    
    private int _oldValue;
    private static readonly int _targetValue = 24;

    private static readonly string _appendix = "°C";

    private bool _targetHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        text.color = Color.red;
        _oldValue = _value;
        text.text = Convert.ToString(_value) + " " + _appendix;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetHit || _value == _oldValue) return;
        
        text.text = Convert.ToString(_value) + " " + _appendix;
        _oldValue = _value;

        
        if (_value != _targetValue) return;
        
        text.color = Color.green;
        _targetHit = true;

    }

    public void ChangeValue(int x)
    {
        if (_targetHit) return;
        _value += x;
        OnAirconValueChange(_value);
    }
    
}

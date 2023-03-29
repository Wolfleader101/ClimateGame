using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Aircon : MonoBehaviour
{
    
    public TextMeshProUGUI text;

    public int value;
    private int oldValue;
    private static int targetvalue = 24;

    private static readonly String appendix = "°C";
    
    // Start is called before the first frame update
    void Start()
    {
        
        oldValue = value;
        text.text = Convert.ToString(value) + " " + appendix;
    }

    // Update is called once per frame
    void Update()
    {
        if (value != oldValue)
        {
            text.text = Convert.ToString(value) + " " + appendix;
            oldValue = value;
            
        }
    }
}

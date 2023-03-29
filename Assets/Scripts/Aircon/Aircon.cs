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

    private bool targetHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        text.color = Color.red;
        oldValue = value;
        text.text = Convert.ToString(value) + " " + appendix;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!targetHit && value != oldValue)
        {
            text.text = Convert.ToString(value) + " " + appendix;
            oldValue = value;

            if (value == targetvalue)
            {
                text.color = Color.green;
                targetHit = true;
            }
        }
        
    }
}

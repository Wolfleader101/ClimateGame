using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RecyclableRubbish : MonoBehaviour
{
    public string rubbishType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RecycleBin") && other.GetComponent<Recyclebin>().recyclingType == rubbishType)
        {
            Destroy(gameObject);
            //other.GetComponent<RecyclingBinScript>().IncrementScore(); <- Maybe func for helping clear fog
        }
    }
}
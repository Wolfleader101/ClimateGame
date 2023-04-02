using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;

public class LightSwitch : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    public GameObject lightSwitch = null;
    public bool modifyValues; //A check for when the lists that store the dictionary data can be cleared

    [SerializeField]
    private DictionaryScriptableObject dictionaryData; //The data that will be shown in the inspector
    [SerializeField]
    private List<GameObject> lightMesh = new List<GameObject>(); //Dictionary Key
    [SerializeField]
    private List<Light> lights = new List<Light>(); //Dictionary value
    private Dictionary<GameObject, Light> lightModels = new Dictionary<GameObject, Light>();

    public void OnAfterDeserialize()
    {
        if(!modifyValues) //Checks if the lists that store the dictionary data can be modified
        {
            lightMesh.Clear(); //Clears the current light meshes
            lights.Clear(); //Clears the current lights

            for (int i = 0; i < Mathf.Min(dictionaryData.LightMesh.Count, dictionaryData.Lights.Count); i++) //Loops through all elements in the dictionary data object and adds them
            {
                lightMesh.Add(dictionaryData.LightMesh[i]);
                lights.Add(dictionaryData.Lights[i]);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void DeserializeDictionary()
    {
        Debug.Log("NOTICE: Deserializing dictionary data for dictionary data object " + dictionaryData.name);

        lightModels = new Dictionary<GameObject, Light>();

        dictionaryData.LightMesh.Clear(); //Clears the current light meshes
        dictionaryData.Lights.Clear(); //Clears the current lights

        for (int i = 0; i < Mathf.Min(lightMesh.Count, lights.Count); i++) //Loops through all elements in the lists and adds them to the dictionary (Based on smallest list size)
        {
            lightMesh.Add(dictionaryData.LightMesh[i]);
            lights.Add(dictionaryData.Lights[i]);

            lightModels.Add(lightMesh[i], lights[i]);
        }

        modifyValues = false;
    }
}

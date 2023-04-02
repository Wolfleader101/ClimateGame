using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Data Object", menuName = "Data Container/Dictionary Data Object")]
public class DictionaryData : SerializedScriptableObject
{
    public Dictionary<GameObject, Light> lightModels; //Holds a pair, one gameobject for one light
}

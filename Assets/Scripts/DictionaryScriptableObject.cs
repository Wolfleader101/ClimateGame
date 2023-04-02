using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Dictionary Storage Object")]
public class DictionaryScriptableObject : ScriptableObject
{
    [SerializeField]
    List<GameObject> lightMesh = new List<GameObject>(); //Dictionary Key
    [SerializeField]
    List<Light> lights = new List<Light>(); //Dictionary value

    public List<GameObject> LightMesh { get => lightMesh; set => lightMesh = value; }
    public List<Light> Lights { get => lights; set => lights = value; }
}

using System;
using System.Collections;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class LightSwitch : MonoBehaviour
    {
        [SerializedDictionary("Light Model", "Point Light")]
        public SerializedDictionary<GameObject, Light> lightModels;
        private GameObject switchObject;

        public void Awake()
        {
            switchObject = this.gameObject;
        }
    }
}

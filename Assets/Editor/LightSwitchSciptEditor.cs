using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightSwitch))]
public class LightSwitchSciptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(((LightSwitch)target).modifyValues)
        {
            if(GUILayout.Button("Save Changes"))
            {
                ((LightSwitch)target).DeserializeDictionary();
            }
        }
    }
}

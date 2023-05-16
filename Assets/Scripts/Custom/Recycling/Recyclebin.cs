using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Collider))]
public class Recyclebin : MonoBehaviour
{
    public event Action OnRubbishIncrement;

    public void IncrementScore()
    {
        OnRubbishIncrement();
    }
}

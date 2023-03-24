using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Recyclebin : MonoBehaviour
{

    //public string recyclingType;

    private int _score = 0;

    public void IncrementScore()
    {
        _score++;
    }
}

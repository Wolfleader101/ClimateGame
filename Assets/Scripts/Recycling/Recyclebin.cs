using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Recyclebin : MonoBehaviour
{

    public string recyclingType;

    public int score = 0;

    public void IncrementScore()
    {
        score++;
    }
}

using System.Collections;
using UnityEngine;

public class LiquidPour : MonoBehaviour
{
    [SerializeField] private float threshold = 50.0f;

    [SerializeField] private Vector2 pourAngle = Vector2.zero;

    [SerializeField] private bool isPouring = false;

    private void Update()
    {
        pourAngle = CalculatePourAngle();

        bool checkX = pourAngle.x > threshold || pourAngle.x < -threshold;
        bool checkZ = pourAngle.y > threshold || pourAngle.y < -threshold;

        if (isPouring != checkX || isPouring != checkZ)
        {
            isPouring = checkX | checkZ;

            if (isPouring)
                BeginPour();
            else
                EndPour();
        }
    }

    private Vector2 CalculatePourAngle()
    {
        return new Vector2(transform.forward.y * Mathf.Rad2Deg, transform.right.y * Mathf.Rad2Deg);
    }

    private void BeginPour()
    {
        
    }

    private void EndPour()
    {
    }
}
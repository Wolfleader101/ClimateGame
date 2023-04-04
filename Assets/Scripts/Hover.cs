using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed = 5.0f;
    
    [Space]

    [SerializeField] private float min = 0.0f;
    [SerializeField] private float max = 1.0f;

    [Space]

    [SerializeField] private float scale = 10.0f;
    
    
    private Vector3 pos;

    private RectTransform tf;

    private void Awake()
    {
        GetTransform();
    }

    private void GetTransform()
    {
        tf = GetComponent<RectTransform>();
        pos = tf.transform.localPosition;
    }
    
    void Update()
    {
        tf.transform.localPosition = new Vector3(pos.x, pos.y + Mathf.Sin(Time.time * speed) * scale, pos.z);
    }
}

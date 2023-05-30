using UnityEngine;
using TMPro;

public class NotepadScript : MonoBehaviour
{
    private Animator animator;
    private bool opened = false;

    private AudioSource source;
    private AudioClip clip;

    [SerializeField] private TMP_Text light_task;
    [SerializeField] private TMP_Text aircon_task;
    [SerializeField] private TMP_Text plant_task;
    [SerializeField] private TMP_Text recycle_task;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            animator.SetBool("opened", opened = true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            animator.SetBool("opened", opened = false);

        if (Input.GetKeyDown(KeyCode.T) && opened)
        {
            OnStrikethrough();
        }
    }

    private void OnStrikethrough()
    {
        source.Play();
    }
}

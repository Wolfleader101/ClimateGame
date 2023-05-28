using UnityEngine;
using TMPro;

public class NotepadScript : MonoBehaviour
{
    private Animator animator;
    private bool opened = false;

    private AudioSource source;
    private AudioClip clip;

    [SerializeField] private TMP_Text task_1_t;
    [SerializeField] private TMP_Text task_2_t;

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
            task_1_t.text = "<s>EXAMPLE TASK 1:</s>";
            OnStrikethrough();
        }
    }

    private void OnStrikethrough()
    {
        source.Play();
    }
}

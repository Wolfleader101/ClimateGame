using UnityEngine;
using TMPro;

public class NotepadScript : MonoBehaviour
{
    private Animator animator;
    private bool opened = false;

    [SerializeField] private TMP_Text task_1_t;
    [SerializeField] private TMP_Text task_2_t;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            animator.SetBool("opened", opened = !opened);

        if (Input.GetKeyDown(KeyCode.T))
            task_1_t.text = "<s>EXAMPLE TASK 1:</s>";
    }
}

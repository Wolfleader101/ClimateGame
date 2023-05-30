using UnityEngine;
using TMPro;

public class NotepadScript : MonoBehaviour
{
    private Animator animator;
    private bool opened = false;

    private AudioSource source;
    private AudioClip clip;

    [SerializeField] private TextMeshProUGUI lightUI;
    [SerializeField] private TextMeshProUGUI plantUI;
    [SerializeField] private TextMeshProUGUI airconUI;
    [SerializeField] private TextMeshProUGUI binUI;

    public House ActiveHouse { get; set; }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            animator.SetBool("opened", opened = true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            animator.SetBool("opened", opened = false);

        if(airconUI)
            airconUI.text = "Aircon Temp: " + ActiveHouse.AirconTemp + "°C";
        
        if(binUI)
            binUI.text = "Rubbish Collected " +  ActiveHouse.RubbishCollected + "/" + ActiveHouse.TotalRubbish;
        
        if(plantUI)
            plantUI.text = "Plants Grown: " + ActiveHouse.PlantsGrown + "/" + ActiveHouse.TotalPlants;
        
        if(lightUI)
            lightUI.text = "Lights Turned Off: " + ActiveHouse.LightsOff + "/" + ActiveHouse.TotalSwitches;

    }

    private void OnStrikethrough()
    {
        source.Play();
    }
}

using TMPro;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private Transform LH_controller;
    [SerializeField] private Canvas canvas;

    [SerializeField] private float minZ = 0.0f;
    [SerializeField] private float maxZ = 0.0f;
    
    [SerializeField] private TextMeshProUGUI lightUI;
    [SerializeField] private TextMeshProUGUI plantUI;
    [SerializeField] private TextMeshProUGUI airconUI;
    [SerializeField] private TextMeshProUGUI binUI;

    
    private Vector3 _eulerRotation;

    
    public House ActiveHouse { get; set; }
    

    private void Start()
    {
    }

    private void Update()
    {
        _eulerRotation = LH_controller.localEulerAngles;

        if (_eulerRotation.z >= minZ && _eulerRotation.z <= maxZ)
            canvas.gameObject.SetActive(true);
        else
            canvas.gameObject.SetActive(false);

        if (ActiveHouse == null)
        {
            if (airconUI)
                airconUI.text = "Not in a House";

            if (binUI)
                binUI.text = "";

            if (plantUI)
                plantUI.text = "";

            if (lightUI)
                lightUI.text = "";
            return;
        }
        
        if(airconUI)
            airconUI.text = "Aircon Temp: " + ActiveHouse.AirconTemp + "°C";
        
        if(binUI)
            binUI.text = "Rubbish Collected " +  ActiveHouse.RubbishCollected + "/" + ActiveHouse.TotalRubbish;
        
        if(plantUI)
            plantUI.text = "Plants Grown: " + ActiveHouse.PlantsGrown + "/" + ActiveHouse.TotalPlants;
        
        if(lightUI)
            lightUI.text = "Lights Turned Off: " + ActiveHouse.LightsOff + "/" + ActiveHouse.TotalSwitches;
    }
}

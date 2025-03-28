using UnityEngine;
using UnityEngine.UI; // Import UI namespace for Image

public class HeatBarManager : MonoBehaviour
{
    [SerializeField] private Image heatBarImage;   
    [SerializeField] private BubbleHeat bubbleHeat; 
    [SerializeField] private float maxHeatValue = 100f; 
    [SerializeField] private float minHeatValue = 0f; 

    void Start()
    {

    }

    void Update()
    {
        if (bubbleHeat != null && heatBarImage != null)
        {
            UpdateHeatBar();
        }
    }

    void UpdateHeatBar()
    {
        float normalizedHeat = Mathf.Clamp01((bubbleHeat.heatValue /100));

        // Update the fill amount of the heat bar
        heatBarImage.fillAmount = normalizedHeat;

        heatBarImage.color = Color.Lerp(Color.blue, Color.red, normalizedHeat); 
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    [Header("Config")]
    public PlantSData plantSData;
    public TextMeshProUGUI plantName;
    public Image plantImage;

    void Start()
    {
        plantName.SetText(plantSData.plantName);
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result as Sprite;
        };
    }
}

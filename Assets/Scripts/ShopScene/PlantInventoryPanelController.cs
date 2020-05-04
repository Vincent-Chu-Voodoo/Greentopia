using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantInventoryPanelController : MonoBehaviour
{
    [Header("Display")]
    public PlantSData plantSData;

    [Header("Config")]
    public Image plantImage;
    public TextMeshProUGUI plantName;
    public TextMeshProUGUI beautyPointText;
    public TextMeshProUGUI countText;

    public void Setup(PlantData plantData)
    {
        plantSData = GameDataManager.instance.GetPlantSData(plantData.plantName);
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result as Sprite;
        };
        plantName.SetText($"{plantSData.plantName}");
        beautyPointText.SetText($"+{plantSData.prestigePoint:0} PRESTIGEPOINTS");
        countText.SetText($"{plantData.count}");
    }
}

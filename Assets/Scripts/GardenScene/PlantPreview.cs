using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class PlantPreview : MonoBehaviour
{
    [Header("Config")]
    public Image plantImage;
    public TextMeshProUGUI plantName;

    public void Setup(PlantSData plantSData)
    {
        plantName.SetText($"{plantSData.plantName}");
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result as Sprite;
        };
    }
}

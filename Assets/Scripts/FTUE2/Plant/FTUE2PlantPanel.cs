using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FTUE2PlantPanel : MonoBehaviour
{
    [Header("Display")]
    public PlantSData plantSData;

    [Header("Config")]
    public Image plantImage;
    public TextMeshProUGUI plantName;
    public TextMeshProUGUI xPText;
    public TextMeshProUGUI countText;
    public GameObject pinGO;
    public FTUE2PlantIngredientPanelController plantIngredientPanelController;

    [Header("Event")]
    public GameEvent OnPin;

    public void Setup(PlantSData _plantSData, int count = 0, bool isPinned = false)
    {
        plantSData = _plantSData;
        ResourceManager.instance.GetPlantSpriteAOH(_plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result;
        };
        plantName.SetText($"{plantSData.plantName}");
        xPText.SetText($"{plantSData.prestigePoint:0}XP");
        countText.SetText($"You have: {count}");
        if (isPinned)
            pinGO.SetActive(false);
        plantIngredientPanelController.Setup(plantSData.ingredientList);
    }

    public void Pin()
    {
        OnPin.Invoke(this);
    }

    //public void UpdateIngredient(List<IngredientData> ingredientDataList)
    //{
    //    plantIngredientPanelController.UpdateIngredient(ingredientDataList);
    //}

    public void RefreshIngredient()
    {
        plantIngredientPanelController.RefreshSatisfication();
    }
}

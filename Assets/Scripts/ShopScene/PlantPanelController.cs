using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantPanelController : MonoBehaviour
{
    [Header("Display")]
    public PlantSData plantSData;

    [Header("Config")]
    public Button craftButton;
    public Image plantImage;
    public TextMeshProUGUI plantName;
    public TextMeshProUGUI beautyPointText;
    public PlantIngredientPanelController plantIngredientPanelController;
    public GameObject prestigeUnlockPanel;
    public TextMeshProUGUI prestigeUnlockPanelText;

    [Header("Event")]
    public GameEvent OnCraft;

    public void Setup(PlantSData _plantSData)
    {
        prestigeUnlockPanelText.SetText($"Prestige level {_plantSData.prestigeLevelRequirement}");
        prestigeUnlockPanel.SetActive(GameDataManager.instance.GetPrestigeLevel() < _plantSData.prestigeLevelRequirement);
        plantSData = _plantSData;
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result as Sprite;
        };
        plantName.SetText($"{plantSData.plantName}");
        beautyPointText.SetText($"+{plantSData.prestigePoint:0} Prestige");
        plantIngredientPanelController.Setup(plantSData.ingredientList);
    }

    public void Craft()
    {
        OnCraft.Invoke(this);
    }

    public void IngredientSatisfied(object isSatisfiedObj)
    {
        IngredientSatisfied((bool)isSatisfiedObj);
    }

    public void IngredientSatisfied(bool isSatisfied = true)
    {
        craftButton.gameObject.SetActive(isSatisfied);
    }
}

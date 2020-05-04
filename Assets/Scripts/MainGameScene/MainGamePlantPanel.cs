using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePlantPanel : MonoBehaviour
{
    [Header("Display")]
    public bool isSatisfied;
    public PlantSData plantSData;

    [Header("Config")]
    public Button craftButton;
    public TextMeshProUGUI plantName;
    public TextMeshProUGUI beautyPointText;
    public PlantIngredientPanelController plantIngredientPanelController;

    [Header("Event")]
    public GameEvent OnCraft;
    public GameEvent OnIngredientSatisfied;

    public Action<MainGamePlantPanel> OnIngredientNewSatisfiedAction;

    public void Setup(PlantSData _plantSData)
    {
        plantSData = _plantSData;
        plantName.SetText($"{plantSData.plantName}");
        beautyPointText.SetText($"+{plantSData.prestigePoint:0} Prestige points");
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

    public void IngredientSatisfied(bool _isSatisfied = true)
    {
        craftButton.gameObject.SetActive(_isSatisfied);
        if (_isSatisfied)
        {
            OnIngredientSatisfied.Invoke(this);
            if (!isSatisfied)
                OnIngredientNewSatisfiedAction?.Invoke(this);
        }
        isSatisfied = _isSatisfied;
    }

    public void UpdateIngredient(List<IngredientData> newIngredienList)
    {
        plantIngredientPanelController.RefreshSatisfication(newIngredienList);
    }
}

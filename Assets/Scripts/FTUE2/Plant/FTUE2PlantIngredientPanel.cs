using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FTUE2PlantIngredientPanel : MonoBehaviour
{
    [Header("Display")]
    public bool isIngredientSatisfied;
    public IngredientData ingredientData;

    [Header("Config")]
    public Image ingredientImage;
    public TextMeshProUGUI ingredientNameText;
    public TextMeshProUGUI requirementText;

    public void Setup(IngredientData _ingredientData)
    {
        ingredientData = _ingredientData;
        ingredientNameText.SetText($"{_ingredientData.atomEnum}");
        ResourceManager.instance.GetAtomSpriteAOH(ingredientData.atomEnum, (int)ingredientData.level).Completed += aoh =>
        {
            ingredientImage.sprite = aoh.Result;
        };
        RefreshRequirement();
    }

    public void RefreshRequirement()
    {
        var have = GameDataManager.instance.GetIngredientCount(ingredientData.atomEnum, ingredientData.level);
        RefreshRequirement(have);
    }

    public void RefreshRequirement(float have)
    {
        var need = ingredientData.count;
        RefreshRequirement(have, need);
    }

    public void RefreshRequirement(float have, float need)
    {
        isIngredientSatisfied = have >= need;
        requirementText.SetText($"{have} / {need}");
        requirementText.color = isIngredientSatisfied ? Color.green : Color.red;
    }
}

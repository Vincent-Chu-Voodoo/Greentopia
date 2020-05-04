using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantIngredientPanel : MonoBehaviour
{
    [Header("Display")]
    public bool isIngredientSatisfied;
    public IngredientData ingredientData;

    [Header("Config")]
    public Image ingredientImage;
    public TextMeshProUGUI requirementText;

    public void Setup(IngredientData _ingredientData)
    {
        ingredientData = _ingredientData;
        var aoh = ResourceManager.instance.GetAtomSpriteAOH(ingredientData.atomEnum, (int)ingredientData.level);
        aoh.Completed += _ =>
        {
            ingredientImage.sprite = aoh.Result as Sprite;
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

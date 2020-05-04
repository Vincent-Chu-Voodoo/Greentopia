using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    [Header("Config")]
    public GameObject ingredientPrefab;
    public Transform ingredientRoot;
    public Image plantImage;
    public Animator craftPanelAnimator;
    public TextMeshProUGUI plantNameText;
    public TextMeshProUGUI prestigePointsText;
    public Button placeButton;
    public Button storeButton;

    [Header("Event")]
    public GameEvent OnCrafted;

    private void Start()
    {
        placeButton.interactable = false;
        storeButton.interactable = false;
    }

    public void Craft(PlantSData plantSData)
    {
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            plantImage.sprite = aoh.Result as Sprite;
        };
        plantNameText.SetText($"{plantSData.plantName}");
        prestigePointsText.SetText($"+{plantSData.prestigePoint} PRESTIGE POINT");
        foreach (var ingredient in plantSData.ingredientList)
        {
            var aoh = ResourceManager.instance.GetAtomSpriteAOH(ingredient.atomEnum, (int) ingredient.level);
            aoh.Completed += _ =>
            {
                var newIngredientDisplay = Instantiate(ingredientPrefab, ingredientRoot);
                newIngredientDisplay.GetComponent<Image>().sprite = aoh.Result as Sprite;
            };
        }
        craftPanelAnimator.Play($"craft");
    }

    public void AE_CraftFinish()
    {
        placeButton.interactable = true;
        storeButton.interactable = true;
        OnCrafted.Invoke(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsPanel : MonoBehaviour
{
    public Image ingredientImage;
    public TextMeshProUGUI ingredientName;
    public TextMeshProUGUI ingredientCount;

    public void Setup(IngredientData ingredient)
    {
        var aoh = ResourceManager.instance.GetAtomSpriteAOH(ingredient.atomEnum, (int) ingredient.level);
        aoh.Completed += _ =>
        {
            ingredientImage.sprite = aoh.Result as Sprite;
        };
        ingredientName.SetText($"{ingredient.atomEnum}\n{ingredient.level:0}");
        ingredientCount.SetText($"x{ingredient.count:0}");
    }
}

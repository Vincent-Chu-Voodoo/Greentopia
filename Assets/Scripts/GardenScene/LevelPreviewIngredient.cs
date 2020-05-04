using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewIngredient : MonoBehaviour
{
    [Header("Config")]
    public Image ingredientImage;

    public void Setup(Sprite ingredientSprite)
    {
        ingredientImage.sprite = ingredientSprite;
    }
}

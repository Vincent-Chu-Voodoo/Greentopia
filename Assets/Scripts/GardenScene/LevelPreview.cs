using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{
    [Header("Config")]
    public AssetReference ingredientAr;
    public Transform ingredientRoot;
    public TextMeshProUGUI levelTitle;

    public void Setup(LevelSData levelSData)
    {
        levelTitle.SetText(string.Format(levelTitle.text, levelSData.level));
        foreach (var spawnerDetail in levelSData.spawnerDetailList)
        {
            ResourceManager.instance.GetAtomSpriteAOH(spawnerDetail.atomEnum, spawnerDetail.atomLevel).Completed += aoh =>
            {
                ingredientAr.InstantiateAsync(ingredientRoot).Completed += ingredientArAoh =>
                {
                    ingredientArAoh.Result.GetComponent<LevelPreviewIngredient>().Setup(aoh.Result as Sprite);
                };
            };
        }
    }
}

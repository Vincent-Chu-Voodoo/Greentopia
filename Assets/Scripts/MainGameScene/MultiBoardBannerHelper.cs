using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBoardBannerHelper : MonoBehaviour
{
    [Header("Param")]
    public IngredientData ingredientData;

    [Header("Config")]
    public GameObject bannerRoot;

    void Start()
    {
        if (GameDataManager.instance.GetPrestigeLevel() != 2 || GameDataManager.instance.gameData.tutorialData.iKnowMultiBoard)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OnAtomCombined(object atomObj)
    {
        OnAtomCombined(atomObj as Atom);
    }

    public void OnAtomCombined(Atom atom)
    {
        if (ingredientData.atomEnum == atom.atomType && Mathf.Abs(atom.atomLevel - ingredientData.level) < 0.1f)
        {
            ingredientData.count--;
        }
        if (ingredientData.count <= 0)
        {
            bannerRoot.SetActive(true);
            GameDataManager.instance.gameData.tutorialData.iKnowMultiBoard = true;
        }
    }
}

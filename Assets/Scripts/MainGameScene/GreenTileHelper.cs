using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreenTileHelper : MonoBehaviour
{
    [Header("Config")]
    public GameObject whatIsGreenTilePanel;
    public GameObject dontMergeGreenTilePanel;
    public TextMeshProUGUI explainText;
    public Image whatIsGreenTileIngredientImage;
    public Image dontMergeGreenTileIngredientImage;

    public void AtomPreCombine(object preCombineParamObj)
    {
        AtomPreCombine(preCombineParamObj as PreCombineParam);
    }

    public void AtomCombine(object atomObj)
    {
        if (!gameObject.activeSelf)
            return;
        AtomCombine(atomObj as Atom);
    }

    public void AtomCombine(Atom atom)
    {
        if (atom.isCanCraft && !GameDataManager.instance.gameData.tutorialData.iKnowWhatIsGreenTile)
        {
            whatIsGreenTileIngredientImage.sprite = atom.atomDisplay.spriteRendererBG.sprite;
            var target = GameDataManager.instance.plantSDataList.Find(i => GameDataManager.instance.GetPrestigeLevel() >= i.prestigeLevelRequirement && i.ingredientList.Exists(j => j.atomEnum == atom.atomType && Mathf.Abs(j.level - atom.atomLevel) < 0.1f));
            print($"akaCK1 {target} {target is null}");
            explainText.SetText(string.Format(explainText.text, target.plantName));
            whatIsGreenTilePanel.SetActive(true);
            GameDataManager.instance.gameData.tutorialData.iKnowWhatIsGreenTile = true;
        }
    }

    public void AtomPreCombine(PreCombineParam preCombineParam)
    {
        var atom = preCombineParam.fromAtom;
        if (atom.isCanCraft && GameDataManager.instance.gameData.tutorialData.iKnowIShouldNotMergeGreenTile-- > 0)
        {
            dontMergeGreenTileIngredientImage.sprite = atom.atomDisplay.spriteRendererBG.sprite;
            dontMergeGreenTilePanel.SetActive(true);
            preCombineParam.canNotCombine = true;
        }
    }
}

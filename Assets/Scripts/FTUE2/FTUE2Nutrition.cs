using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTUE2Nutrition : MonoBehaviour
{
    public FTUE2Plant plant;
    public GameObject haveGO;
    public GameObject haventGO;
    public Button button;

    void Start()
    {
        UpdateHave(HasEnoughIngredients());
    }

    public bool HasEnoughIngredients()
    {
        foreach (var ingredientData in plant.plantSData.nutrientRequiredList)
        {
            if (GameDataManager.instance.gameData.nurserySessionData.gridDataList.Exists(
                i => !i.isCrate && !i.isDusty
                && i.atomEnum == ingredientData.atomEnum
                && i.atomLevel == ingredientData.level
                ))
                continue;
            if (GameDataManager.instance.gameData.toolShedSessionData.gridDataList.Exists(
                i => !i.isCrate && !i.isDusty
                && i.atomEnum == ingredientData.atomEnum
                && i.atomLevel == ingredientData.level
                ))
                continue;
            return false;
        }
        return true;
    }

    public void UpdateHave(bool have)
    {
        haveGO.SetActive(have);
        haventGO.SetActive(!have);
        button.enabled = have;
    }
}

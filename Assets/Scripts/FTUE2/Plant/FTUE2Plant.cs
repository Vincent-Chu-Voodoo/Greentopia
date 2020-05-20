using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FTUE2Plant : MonoBehaviour
{
    public int plantStage;
    public PlantSData plantSData;
    public FTUE2PlantDisplay plantDisplay;

    public void Setup(PlantSData _plantSData, int _plantStage)
    {
        plantSData = _plantSData;
        plantStage = _plantStage;
        plantDisplay.Setup(_plantSData, plantStage);
    }

    public void Planted()
    {
        foreach (var ingredient in plantSData.ingredientList)
        {
            var remainCount = ingredient.count;
            var allIngredientList = GameDataManager.instance.gameData.nurserySessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                );
            while (ingredient.count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.nurserySessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }

            allIngredientList = GameDataManager.instance.gameData.toolShedSessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                );
            while (ingredient.count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.toolShedSessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }
        }

        GameDataManager.instance.gameData.gardentPlantList.Add(
            new GardenPlantData()
            {
                id = 0,
                plantData = new PlantData()
                {
                    plantName = plantSData.plantName,
                    count = 1
                },
                plantStage = plantStage,
                localPosition = transform.localPosition,
                localScale = transform.localScale
            }
        );

        Destroy(gameObject.GetComponent<FTUE2PlantOnCardBehaviour>());
        gameObject.AddComponent<FTUE2PlantNeedIngredientBehaviour>();
    }
}

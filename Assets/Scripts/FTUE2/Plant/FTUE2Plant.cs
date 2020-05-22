using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum PlantStageEnum
{
    OnCard, NeedNutrition, Growing, Grown, Collected
}

public class FTUE2Plant : MonoBehaviour
{
    public int plantID;
    public int plantStage;
    public PlantStageEnum plantStageEnum;
    public PlantSData plantSData;
    public FTUE2PlantDisplay plantDisplay;
    public Canvas canvas;

    public GameObject nutritionGO;
    public GameObject progressionGO;
    public FTUEPlantProgress fTUEPlantProgress;

    public List<IngredientData> nutrientRequiredList;

    public GameEvent OnPlanted;

    public void Setup(int _id, PlantSData _plantSData, int _plantStage, PlantStageEnum _plantStageEnum)
    {
        plantID = _id;
        plantSData = _plantSData;
        plantStage = _plantStage;
        plantStageEnum = _plantStageEnum;
        plantDisplay.Setup(_plantSData, _plantStage);
        nutrientRequiredList = _plantSData.nutrientRequiredList;
        fTUEPlantProgress.totalTime = plantSData.growningTimeInSecond;
        fTUEPlantProgress.OnGrown.AddListener(Grow);
        canvas.worldCamera = Camera.main;
        ConfigurePlant(_plantStageEnum);
    }

    public void ConfigurePlant(PlantStageEnum _plantStageEnum)
    {
        plantStageEnum = _plantStageEnum;
        switch (plantStageEnum)
        {
            case PlantStageEnum.OnCard:
                gameObject.AddComponent<FTUE2PlantOnCardBehaviour>();
                break;
            case PlantStageEnum.NeedNutrition:
                nutritionGO.SetActive(true);
                break;
            case PlantStageEnum.Growing:
                break;
            case PlantStageEnum.Grown:
                plantDisplay.Setup(plantSData, 10);
                for (var i = 0; i < plantSData.sellableCapacity; i++)
                    plantDisplay.SpawnSellable(plantSData);
                break;
            case PlantStageEnum.Collected:
                plantDisplay.Setup(plantSData, 10);
                break;
            default:
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        switch (plantStageEnum)
        {
            case PlantStageEnum.OnCard:
                break;
            case PlantStageEnum.NeedNutrition:
                break;
            case PlantStageEnum.Growing:
                progressionGO.SetActive(!progressionGO.activeSelf);
                break;
            case PlantStageEnum.Grown:
                Collect();
                break;
            default:
                break;
        }
    }

    public void Collect()
    {
        GameDataManager.instance.gameData.sellableList.Add(plantSData.sellable);
        plantDisplay.CollectSellable();
        ConfigurePlant(PlantStageEnum.Collected);
    }

    public void Grow(object obj)
    {
        progressionGO.SetActive(false);
        ConfigurePlant(PlantStageEnum.Grown);
        GameObject.FindGameObjectWithTag(TagEnum.Header.ToString()).GetComponent<FTUE2Header>().AddXp(plantSData.prestigePoint);

        foreach (var ingredient in plantSData.nutrientRequiredList)
        {
            var remainCount = ingredient.count;
            var allIngredientList = GameDataManager.instance.gameData.nurserySessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                ).ToList();
            while (allIngredientList.Count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.nurserySessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }

            allIngredientList = GameDataManager.instance.gameData.toolShedSessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                );
            while (allIngredientList.Count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.toolShedSessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }
        }
    }

    public void FedNutritionClicked()
    {
        nutritionGO.SetActive(false);
        ConfigurePlant(PlantStageEnum.Growing);
    }

    public void Planted(object obj)
    {
        foreach (var ingredient in plantSData.ingredientList)
        {
            var remainCount = ingredient.count;
            var allIngredientList = GameDataManager.instance.gameData.nurserySessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                ).ToList();
            while (allIngredientList.Count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.nurserySessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }

            allIngredientList = GameDataManager.instance.gameData.toolShedSessionData.gridDataList.FindAll(
                i => !i.isDusty && i.atomEnum == ingredient.atomEnum && i.atomLevel == ingredient.level
                );
            while (allIngredientList.Count > 0 && remainCount > 0)
            {
                GameDataManager.instance.gameData.toolShedSessionData.gridDataList.Remove(allIngredientList[0]);
                allIngredientList.RemoveAt(0);
                remainCount--;
            }
        }

        GameDataManager.instance.gameData.gardentPlantList.Add(
            new GardenPlantData()
            {
                id = plantID,
                plantData = new PlantData()
                {
                    plantName = plantSData.plantName,
                    count = 1
                },
                plantStageEnum = plantStageEnum,
                plantStage = plantStage,
                localPosition = transform.localPosition,
                localScale = transform.localScale
            }
        );

        Destroy(gameObject.GetComponent<FTUE2PlantOnCardBehaviour>());
        ConfigurePlant(PlantStageEnum.NeedNutrition);

        OnPlanted.Invoke(this);
    }
}

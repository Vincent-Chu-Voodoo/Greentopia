using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class GardenSceneParam
{
    public PlantSData craftedPlant;
}

public class GardenSceneController : MonoBehaviour
{
    [Header("Display")]
    public List<Plant> plantList;

    [Header("Config")]
    public GameObject plantPrefab;
    public AssetReference shopSceneAR;
    public AssetReference playPanelSceneAR;
    public Transform plantRoot;
    public PrestigePointController prestigePointController;
    public SceneTransition sceneTransition;

    public void Awake()
    {
        InitializePrestigePoint();
        InitializePlantsFromGameData();
        if (GameDataManager.instance.sceneParam is GardenSceneParam)
            ProcessGardenSceneParam(GameDataManager.instance.sceneParam as GardenSceneParam);
        GameDataManager.instance.sceneParam = null;
    }

    public void ProcessGardenSceneParam(GardenSceneParam gardenParam)
    {
        if (gardenParam.craftedPlant != null)
            ProcessNewCraftedPlant(gardenParam.craftedPlant);
    }

    public void ProcessNewCraftedPlant(PlantSData plantSData)
    {
        if (GameDataManager.instance.gameData.gardentPlantList == null)
            GameDataManager.instance.gameData.gardentPlantList = new List<GardenPlantData>();
        var id = GameDataManager.instance.gameData.gardentPlantList.Count;
        var newPlantGO = Instantiate(plantPrefab, plantRoot);
        var newPlant = newPlantGO.GetComponent<Plant>();
        newPlant.Setup(plantSData, id, newSpawn: true);
        GameDataManager.instance.gameData.gardentPlantList.Add(
            new GardenPlantData()
            {
                id = id,
                plantData = new PlantData()
                {
                    plantName = plantSData.plantName,
                    count = 1
                },
                localPosition = newPlant.transform.localPosition,
                localScale = newPlant.transform.localScale
            }
        );
        AddPrestigePoint(plantSData.prestigePoint);
    }

    public void InitializePrestigePoint()
    {
        prestigePointController.ToPrestigePoint(GameDataManager.instance.gameData.prestigePoint);
    }

    public void InitializePlantsFromGameData()
    {
        foreach (var gardenPlant in GameDataManager.instance.gameData.gardentPlantList)
        {
            for (var i = 0; i < gardenPlant.plantData.count; i++) {
                var newPlantGO = Instantiate(plantPrefab, plantRoot);
                newPlantGO.GetComponent<Plant>().Setup(gardenPlant.plantData.plantName, gardenPlant.id);
                newPlantGO.transform.localPosition = gardenPlant.localPosition;
                newPlantGO.transform.localScale = gardenPlant.localScale;
            }
        }
    }

    public void AddPrestigePoint(float prestigePoint)
    {
        GameDataManager.instance.gameData.prestigePoint += prestigePoint;
        prestigePointController.LerpToPrestigePoint(GameDataManager.instance.gameData.prestigePoint);
    }

    public void Play()
    {
        GameDataManager.instance.sceneParam = new PlayerPanelSceneParam()
        {
            prestigeLevel = GameDataManager.instance.GetPrestigeLevel()
        };
        sceneTransition.FadeIn(() =>
        {
            Addressables.LoadSceneAsync(playPanelSceneAR, UnityEngine.SceneManagement.LoadSceneMode.Single, false).Completed += aoh =>
            {
                aoh.Result.ActivateAsync();
            };
        });
    }

    public void Shop()
    {
        sceneTransition.FadeIn(shopSceneAR);
    }
}

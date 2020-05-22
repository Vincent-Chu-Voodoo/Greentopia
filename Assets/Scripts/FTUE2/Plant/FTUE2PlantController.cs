using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2PlantController : MonoBehaviour
{
    public GameObject plantPrefab;
    public List<FTUE2Plant> allPlantList;
    
    void Start()
    {
        SpawnPlants(GameDataManager.instance.gameData.gardentPlantList);
    }

    private void OnDestroy()
    {
        foreach (var plant in allPlantList)
        {
            var p = GameDataManager.instance.gameData.gardentPlantList.Find(
                i => i.id == plant.plantID
            );
            p.plantStageEnum = plant.plantStageEnum;
        }
    }

    private void SpawnPlants(List<GardenPlantData> gardenPlantDataList)
    {
        foreach (var gardenPlantData in gardenPlantDataList)
            SpawnPlant(gardenPlantData);
    }

    private FTUE2Plant SpawnPlant(GardenPlantData gardenPlantData)
    {
        var plantSData = GameDataManager.instance.GetPlantSData(gardenPlantData.plantData.plantName);
        var newPlant = Instantiate(plantPrefab, transform);
        newPlant.GetComponent<FTUE2Plant>().Setup(gardenPlantData.id, plantSData, gardenPlantData.plantStage, gardenPlantData.plantStageEnum);
        newPlant.transform.localPosition = gardenPlantData.localPosition;
        allPlantList.Add(newPlant.GetComponent<FTUE2Plant>());
        return newPlant.GetComponent<FTUE2Plant>();
    }

    public FTUE2Plant SpawnPlant(PlantSData plantSData, int stage)
    {
        var newPlant = Instantiate(plantPrefab, transform);
        newPlant.GetComponent<FTUE2Plant>().Setup(allPlantList.Count, plantSData, stage, PlantStageEnum.OnCard);
        allPlantList.Add(newPlant.GetComponent<FTUE2Plant>());
        return newPlant.GetComponent<FTUE2Plant>();
    }
}

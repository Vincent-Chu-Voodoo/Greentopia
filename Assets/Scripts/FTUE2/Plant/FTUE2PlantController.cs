using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2PlantController : MonoBehaviour
{
    public GameObject plantPrefab;
    
    void Start()
    {
        SpawnPlants(GameDataManager.instance.gameData.gardentPlantList);
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
        newPlant.GetComponent<FTUE2Plant>().Setup(plantSData, gardenPlantData.plantStage);
        return newPlant.GetComponent<FTUE2Plant>();
    }

    public FTUE2Plant SpawnPlant(PlantSData plantSData, int stage)
    {
        var newPlant = Instantiate(plantPrefab, transform);
        newPlant.GetComponent<FTUE2Plant>().Setup(plantSData, stage);
        return newPlant.GetComponent<FTUE2Plant>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2PlantController : MonoBehaviour
{
    public AssetReference plantAR;
    
    void Start()
    {
        SpawnPlants(GameDataManager.instance.gameData.gardentPlantList);
    }

    private void SpawnPlants(List<GardenPlantData> gardenPlantDataList)
    {
        foreach (var gardenPlantData in gardenPlantDataList)
            SpawnPlant(gardenPlantData);
    }

    private void SpawnPlant(GardenPlantData gardenPlantData)
    {
        var plantSData = GameDataManager.instance.GetPlantSData(gardenPlantData.plantData.plantName);
        plantAR.InstantiateAsync(transform).Completed += aoh =>
        {
            aoh.Result.GetComponent<FTUE2Plant>().Setup(plantSData);
        };
    }

    void Update()
    {
        
    }
}

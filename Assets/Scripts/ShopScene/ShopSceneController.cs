using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class ShopSceneParam
{
    //public PlantSData plantSData;
}


public class ShopSceneController : MonoBehaviour
{
    [Header("Config")]
    public AssetReference closeTargetAR;
    public AssetReference craftTargetAR;
    //public List<LevelSData> levelDataList;
    //public List<AtomDisplaySData> atomDisplaySDataList;
    //public AssetReference levelPanelAR;
    //public Transform levelPanelRoot;
    public SceneTransition sceneTransition;

    [Header("Event")]
    public GameEvent OnCraft;

    void Start()
    {
        if (GameDataManager.instance.sceneParam is ShopSceneParam)
            ProcessShopSceneParam(GameDataManager.instance.sceneParam as ShopSceneParam);
        GameDataManager.instance.sceneParam = null;
    }

    public void ProcessShopSceneParam(ShopSceneParam shopSceneParam)
    {
        //var targetPlantIndex = GameDataManager.instance.gameData.inventoryPlantList.FindIndex(i => i.plantName == shopSceneParam.plantSData.plantName);
        //if (targetPlantIndex != -1)
        //    GameDataManager.instance.gameData.inventoryPlantList[targetPlantIndex].count++;
        //else
        //    GameDataManager.instance.gameData.inventoryPlantList.Add(new PlantData()
        //    {
        //        plantName = shopSceneParam.plantSData.plantName,
        //        count = 1
        //    });
    }

    //public void Play(object levelPanelObj)
    //{
    //    Play(levelPanelObj as LevelPanel);
    //}

    //public void Play(LevelPanel levelPanel)
    //{
    //    print($"Play {levelPanel.levelSData.level}");
    //    Addressables.LoadSceneAsync(playTargetScene.ToString());
    //}

    public void Craft(PlantSData plantSData)
    {
        GameDataManager.instance.sceneParam = new CraftSceneParam()
        {
            plantSData = plantSData
        };
        sceneTransition.FadeIn(craftTargetAR);
        OnCraft.Invoke(plantSData);
    }

    public void Close()
    {
        sceneTransition.FadeIn(closeTargetAR);
    }
}

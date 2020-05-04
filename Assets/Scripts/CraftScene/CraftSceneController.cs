using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class CraftSceneParam {
    public PlantSData plantSData;
}

public class CraftSceneController : MonoBehaviour
{
    [Header("Display")]
    public PlantSData plantSData;

    [Header("Config")]
    public AssetReference closeTargetSceneAR;
    public AssetReference placeTargetSceneAR;
    //public AssetReference storeTargetSceneAR;
    public CraftPanel craftPanel;
    public SceneTransition sceneTransition;

    void Start()
    {
        if (GameDataManager.instance.sceneParam is CraftSceneParam)
            ProcessCraftSceneParam(GameDataManager.instance.sceneParam as CraftSceneParam);
        GameDataManager.instance.sceneParam = null;
    }

    private void ProcessCraftSceneParam(CraftSceneParam caftSceneParam)
    {
        plantSData = caftSceneParam.plantSData;
        craftPanel.Craft(plantSData);
    }

    public void Crafted()
    {
        var levelSessionDataList = GameDataManager.instance.gameData.levelSessionDataList;
        var newIngredienList = new List<IngredientData>(plantSData.ingredientList);
        for (var j = 0; j < newIngredienList.Count; j++)
        {
            var countRemain = newIngredienList[j].count;
            for (var i = 0; i < levelSessionDataList.Count; i++)
            {
                var ingredient = newIngredienList[j];
                var index = levelSessionDataList[i].gridDataList.FindIndex(
                    k => k.atomEnum == ingredient.atomEnum && k.atomLevel == ingredient.level);

                while (index != -1)
                {
                    levelSessionDataList[i].gridDataList.RemoveAt(index);
                    //GameDataManager.instance.SaveSession(levelSessionDataList[i]);
                    if (--countRemain <= 0)
                        break;
                    index = levelSessionDataList[i].gridDataList.FindIndex(
                    k => k.atomEnum == ingredient.atomEnum && k.atomLevel == ingredient.level);
                }

                if (countRemain <= 0)
                    break;
            }
        }
    }

    public void Place()
    {
        GameDataManager.instance.sceneParam = new GardenSceneParam()
        {
            craftedPlant = plantSData
        };
        sceneTransition.FadeIn(placeTargetSceneAR);
    }

    //public void Store()
    //{
    //    GameDataManager.instance.sceneParam = new ShopSceneParam()
    //    {
    //        plantSData = plantSData
    //    };
    //    Addressables.LoadSceneAsync(storeTargetSceneAR);
    //}

    public void Close()
    {
        sceneTransition.FadeIn(closeTargetSceneAR);
    }
}

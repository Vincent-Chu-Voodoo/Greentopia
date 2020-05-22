using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : Singleton<ResourceManager>
{
    public AsyncOperationHandle<Sprite> GetPlantSpriteAOH(string plantName, int grownStage = 1)
    {
        return Addressables.LoadAssetAsync<Sprite>($"{plantName}_{grownStage}");
    }

    public AsyncOperationHandle<Sprite> GetAtomSpriteAOH(AtomEnum atomEnum, int atomLevel, bool isDusty = false)
    {
        var target = "";
        if (isDusty)
            target = $"{atomEnum}_{atomLevel}_d";
        else
            target = $"{atomEnum}_{atomLevel}";
        return Addressables.LoadAssetAsync<Sprite>(target);
    }

    //public AsyncOperationHandle GetPlantSData(string sDataName)
    //{
    //    return Addressables.LoadAssetAsync<PlantSData>(sDataName);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : Singleton<ResourceManager>
{
    public AsyncOperationHandle<Sprite> GetPlantSpriteAOH(string plantName)
    {
        return Addressables.LoadAssetAsync<Sprite>(plantName);
    }

    public AsyncOperationHandle<Sprite> GetAtomSpriteAOH(AtomEnum atomEnum, int atomLevel)
    {
        var target = $"{atomEnum}_{atomLevel}";
        return Addressables.LoadAssetAsync<Sprite>(target);
    }

    //public AsyncOperationHandle GetPlantSData(string sDataName)
    //{
    //    return Addressables.LoadAssetAsync<PlantSData>(sDataName);
    //}
}

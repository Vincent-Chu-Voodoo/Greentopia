using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlantSDataBootstrap : MonoBehaviour
{
    [Header("Config")]
    public BootstrapSceneController bootstrapSceneController;
    public List<AssetReference> plantSDataListARList;
    //public List<PlantSData> plantSDataList;

    void Start()
    {
        foreach (var plantSDataListAR in plantSDataListARList)
        {
            bootstrapSceneController?.Lock();
            plantSDataListAR.LoadAssetAsync<PlantSData>().Completed += aoh =>
            {
                GameDataManager.instance.plantSDataList.Add(aoh.Result);
                GameDataManager.instance.plantSDataList.Sort((i, j) => i.prestigeLevelRequirement.CompareTo(j.prestigeLevelRequirement));
                bootstrapSceneController?.UnLock();
            };
        }
        //GameDataManager.instance.plantSDataList = plantSDataList;
    }
}

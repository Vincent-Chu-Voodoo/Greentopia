using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelSDataBootstrap : MonoBehaviour
{
    [Header("Config")]
    public BootstrapSceneController bootstrapSceneController;
    public List<AssetReference> levelSDataARList;
    //public List<LevelSData> levelSDataList;

    void Start()
    {
        //GameDataManager.instance.ClearAllData();
        foreach (var levelSDataAR in levelSDataARList)
        {
            bootstrapSceneController?.Lock();
            levelSDataAR.LoadAssetAsync<LevelSData>().Completed += aoh =>
            {
                GameDataManager.instance.levelSDataList.Add(aoh.Result);
                GameDataManager.instance.levelSDataList.Sort((i, j) => i.level.CompareTo(j.level));
                bootstrapSceneController?.UnLock();
            };
        }
        //GameDataManager.instance.levelSDataList = levelSDataList;
    }
}

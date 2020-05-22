using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PrestigePointSDataBootstrap : MonoBehaviour
{
    [Header("Config")]
    public BootstrapSceneController bootstrapSceneController;
    public AssetReference prestigePointSDataAR;
    //public PrestigePointSData prestigePointSData;

    void Start()
    {
        bootstrapSceneController?.Lock();
        prestigePointSDataAR.LoadAssetAsync<PrestigePointSData>().Completed += aoh =>
        {
            GameDataManager.instance.prestigePointSData = aoh.Result;
            bootstrapSceneController?.UnLock();
        };
    }
}

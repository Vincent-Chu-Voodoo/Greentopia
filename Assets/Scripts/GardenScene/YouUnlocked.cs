using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class YouUnlocked : MonoBehaviour
{
    [Header("Config")]
    public AssetReference plantPreviewAR;
    public AssetReference levelPreviewAR;
    public Transform previewRoot;

    public void Setup(List<PlantSData> plantSDataList, List<LevelSData> levelSDataList)
    {
        foreach (var plantSData in plantSDataList) 
        {
            plantPreviewAR.InstantiateAsync(previewRoot).Completed += aoh =>
            {
                aoh.Result.GetComponent<PlantPreview>().Setup(plantSData);
            };
        }
        foreach (var levelSData in levelSDataList)
        {
            levelPreviewAR.InstantiateAsync(previewRoot).Completed += aoh =>
            {
                aoh.Result.GetComponent<LevelPreview>().Setup(levelSData);
            };
        }
    }
}

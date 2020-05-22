using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUENeighbourSceneController : MonoBehaviour
{
    public static bool haveApple;

    public AssetReference newGardenSceneAR;
    public AssetReference part2SceneAR;

    public GameEvent OnEnterNoApple;
    public GameEvent OnEnterHaveApple;
    public GameEvent OnClaim;

    void Start()
    {
        if (haveApple)
            OnEnterHaveApple.Invoke(this);
        else
        {
            if (FTUEGardenPlant.fTUEGardenPlantStatus == FTUEGardenPlantStatus.Grown)
                FTUEGardenPlant.fTUEGardenPlantStatus = FTUEGardenPlantStatus.CanCollectApple;
            OnEnterNoApple.Invoke(this);
        }
    }

    public void Close()
    {
        newGardenSceneAR.LoadSceneAsync();
    }

    public void Claim()
    {
        OnClaim.Invoke(this);
    }

    public void Part2Start()
    {
        newGardenSceneAR = part2SceneAR;
        var nsd = GameDataManager.instance.gameData.nurserySessionData;
        var tssd = GameDataManager.instance.gameData.toolShedSessionData;
        NewSpawner.cottonCurrentCoolDown = 0f;
        print($"fiveteenPosition: {FTUEGardenPlant.fiveteenPosition}");
        GameDataManager.instance.gameData.gardentPlantList.Add(
            new GardenPlantData()
            {
                id = 0,
                plantData = new PlantData()
                {
                    plantName = "Apple Tree",
                    count = 1
                },
                plantStage = 10,
                plantStageEnum = PlantStageEnum.Collected,
                localPosition = new Vector3(FTUEGardenPlant.fiveteenPosition.x, FTUEGardenPlant.fiveteenPosition.y, 0f) + new Vector3(0.2f, -1.5f, 0f),
                localScale = Vector3.one
            }
        );
        //nsd.gridDataList.RemoveAll(i => i.atomEnum == AtomEnum.apple_sapling && i.isDusty == false);
        //nsd.gridDataList.RemoveAll(i => i.atomEnum == AtomEnum.cotton && i.isDusty == false);
        //tssd.gridDataList.RemoveAll(i => i.atomEnum == AtomEnum.fertiliser && i.isDusty == false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUENeighbourSceneController : MonoBehaviour
{
    public static bool haveApple;

    public AssetReference newGardenSceneAR;

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
}

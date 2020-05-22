using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootstrapSceneControllerSpecial : MonoBehaviour
{
    [Header("Display")]
    public int lockCount;

    [Header("Config")]
    public AssetReference targetScene;
    public PlantSData tomatoPlantSData;

    [Header("Event")]
    public GameEvent OnLoadStart;
    public GameEvent OnLoadFinished;

    IEnumerator Start()
    {
        GameDataManager.instance.ClearAllData();
        GameDataManager.instance.gameData.nurserySessionData.gridDataList.Add(
            new GridData()
            {
                atomEnum = AtomEnum.tomato_sapling,
                totalCoolDown = 0f,
                atomLevel = 4,
                rowIndex = 0,
                columnIndex = 0,
                isDusty = false,
                isCrate = false
            }
        );
        GameDataManager.instance.gameData.nurserySessionData.gridDataList.Add(
            new GridData()
            {
                atomEnum = AtomEnum.cotton,
                totalCoolDown = 0f,
                atomLevel = 4,
                rowIndex = 0,
                columnIndex = 0,
                isDusty = false,
                isCrate = false
            }
        );
        GameDataManager.instance.gameData.nurserySessionData.gridDataList.Add(
            new GridData()
            {
                atomEnum = AtomEnum.trowel,
                totalCoolDown = 0f,
                atomLevel = 4,
                rowIndex = 0,
                columnIndex = 0,
                isDusty = false,
                isCrate = false
            }
        );
        GameDataManager.instance.gameData.nurserySessionData.gridDataList.Add(
            new GridData()
            {
                atomEnum = AtomEnum.fertiliser,
                totalCoolDown = 0f,
                atomLevel = 5,
                rowIndex = 0,
                columnIndex = 0,
                isDusty = false,
                isCrate = false
            }
        );
        GameDataManager.instance.gameData.pinnedPlant = tomatoPlantSData;
        FTUE2Tutorial.tutorialIndex = -1;
        yield return new WaitForSeconds(0.2f);
        LoadNext();
    }

    private void OnDisable()
    {
        OnLoadFinished.Invoke(this);
    }

    public void Lock()
    {
        lockCount++;
    }

    public void UnLock()
    {
        if (--lockCount == 0)
            LoadNext();
    }

    public void LoadNext()
    {
        Application.targetFrameRate = 60;
        Addressables.LoadSceneAsync(targetScene);
        OnLoadStart.Invoke(this);
    }
}

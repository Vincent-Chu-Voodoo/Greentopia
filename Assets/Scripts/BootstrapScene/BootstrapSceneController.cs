using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootstrapSceneController : MonoBehaviour
{
    [Header("Display")]
    public int lockCount;

    [Header("Config")]
    public AssetReference targetScene;

    [Header("Event")]
    public GameEvent OnLoadStart;
    public GameEvent OnLoadFinished;

    IEnumerator Start()
    {
        GameDataManager.instance.ClearAllData();
        yield return null;
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

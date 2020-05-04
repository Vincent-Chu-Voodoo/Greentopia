using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadingSceneController : MonoBehaviour
{
    [Header("Display")]
    public AsyncOperationHandle targetSceneAOH;

    [Header("Config")]
    public AssetReference loadingScreenAR;
    public AssetReference targetScene;
    public SceneTransition sceneTransition;

    [Header("Event")]
    public GameEvent OnLoadStart;
    public GameEvent OnLoadFinished;

    void Start()
    {
        loadingScreenAR.InstantiateAsync().Completed += aoh =>
        {
            OnLoadFinished.AddListener(aoh.Result.GetComponent<LoadingScreen>().LoadFinished);
        };
        
        targetSceneAOH = targetScene.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single, false);
        targetSceneAOH.Completed += aoh =>
        {
            OnLoadFinished.Invoke(this);
            sceneTransition.FadeIn((SceneInstance) aoh.Result);
        };
    }
}

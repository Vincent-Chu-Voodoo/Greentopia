using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneTransition : MonoBehaviour
{
    [Header("Display")]
    public AsyncOperationHandle<SceneInstance> sceneAoh;

    [Header("Param")]
    public bool isStartAsLoading;
    public bool isStartAsLoaded;

    [Header("Config")]
    public Animator animator;

    [Header("Event")]
    public GameEvent OnFadeOut;
    public GameEvent OnFadeIn;
    public GameEvent OnFinishFadeIn;

    public void Start()
    {
        if (isStartAsLoading)
            animator.Play("fade-in", 0, 1f);
        if (isStartAsLoaded)
            animator.Play("fade-out", 0, 1f);
        OnFadeOut.Invoke(this);
    }

    public void FadeIn(SceneInstance sceneInstance)
    {
        OnFinishFadeIn.AddListener(_ =>
        {
            sceneInstance.ActivateAsync();
        });
        OnFadeIn.Invoke(this);
    }

    public void FadeIn(AssetReference sceneAR)
    {
        sceneAoh = Addressables.LoadSceneAsync(sceneAR, UnityEngine.SceneManagement.LoadSceneMode.Single, false);
        OnFinishFadeIn.AddListener(_ =>
        {
            if (sceneAoh.IsDone)
                sceneAoh.Result.ActivateAsync();
            else
                sceneAoh.Completed += __ => sceneAoh.Result.ActivateAsync();
        });
        OnFadeIn.Invoke(this);
    }

    public void FadeIn(Action _callBack)
    {
        OnFinishFadeIn.AddListener(_ => _callBack?.Invoke());
        OnFadeIn.Invoke(this);
    }

    public void FinishFadeIn()
    {
        OnFinishFadeIn.Invoke(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RootSceneController : MonoBehaviour
{
    public AssetReference targetScene;

    void Start()
    {
        targetScene.LoadSceneAsync();
    }
}

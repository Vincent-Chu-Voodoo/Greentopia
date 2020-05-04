using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class BackgroundImageLoader : MonoBehaviour
{
    public Image image;
    public AssetReference imageAR;

    void Start()
    {
        imageAR.LoadAssetAsync<Sprite>().Completed += aoh => {
            image.sprite = aoh.Result;
        };
    }
}

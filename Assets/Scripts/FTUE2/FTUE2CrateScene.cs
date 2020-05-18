using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2CrateScene : MonoBehaviour
{
    public AssetReference backAR;

    public void BackToHome()
    {
        backAR.LoadSceneAsync();
    }
}

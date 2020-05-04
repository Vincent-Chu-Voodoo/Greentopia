using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBannerHelper : MonoBehaviour
{
    void Start()
    {
        if (GameDataManager.instance.gameData.tutorialData.iKnowHowToMerge)
            Destroy(gameObject);
        GameDataManager.instance.gameData.tutorialData.iKnowHowToMerge = true;
    }
}

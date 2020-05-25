using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2NutritionAnimationProxy : MonoBehaviour
{
    public FTUEGardenNutritionAnimation fTUEGardenNutritionAnimation;

    public void Play(Transform anchor)
    {
        fTUEGardenNutritionAnimation.targetAnchor = anchor;
        fTUEGardenNutritionAnimation.gameObject.SetActive(true);
    }
}

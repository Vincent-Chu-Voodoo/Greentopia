using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PlantDisplay : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void Setup(PlantSData plantSData, int growStage)
    {
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.name, growStage).Completed += aoh =>
        {
            spriteRenderer.sprite = aoh.Result;
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PlantDisplay : MonoBehaviour
{
    public void Setup(PlantSData plantSData, int growStage)
    {
        var currentSprite = ResourceManager.instance.GetPlantSprite(plantSData.name, growStage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2Plant : MonoBehaviour
{
    public int plantStage;
    public PlantSData plantSData;
    public FTUE2PlantDisplay plantDisplay;

    public void Setup(PlantSData _plantSData, int _plantStage)
    {
        plantSData = _plantSData;
        plantStage = _plantStage;
        plantDisplay.Setup(_plantSData, plantStage);
    }
}

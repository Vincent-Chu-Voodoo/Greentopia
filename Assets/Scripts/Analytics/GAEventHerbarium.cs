using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAEventHerbarium : MonoBehaviour
{
    [Header("Param")]
    public string herbariumCraftPrefix;

    [Header("Event")]
    public GAEvent gAEvent;

    public void Craft(object plantSDataObj)
    {
        gAEvent.SendDesignEvent($"{herbariumCraftPrefix}{(plantSDataObj as PlantSData).plantName}");
    }
}

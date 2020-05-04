using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAEventMainGame : MonoBehaviour
{
    public int level;

    public string boardsPlayPrefix;
    public string boardsClosePrefix;
    public string boardsCraftPrefix;

    public GAEvent gAEvent;

    public GameEvent OnFirstTimeEnter;
    public GameEvent OnFirstTimeExit;

    public void Initialized(object mainGameControllerGo)
    {
        level = (mainGameControllerGo as MainGameController).levelSessionData.level;
        if (level == 1)
            OnFirstTimeEnter.Invoke(this);
        gAEvent.SendDesignEvent($"{boardsPlayPrefix}{level}");
    }

    public void Close()
    {
        if (level == 1)
            OnFirstTimeExit.Invoke(this);
        gAEvent.SendDesignEvent($"{boardsClosePrefix}{level}");
    }

    public void Craft(object plantSDataObj)
    {
        gAEvent.SendDesignEvent($"{boardsCraftPrefix}{(plantSDataObj as PlantSData).plantName}");
    }
}

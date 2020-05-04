using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAEventGarden : MonoBehaviour
{
    public string progressionStatusPrefix;

    public GAEvent gAEvent;
    
    public void Levelup(object prestigePointControllerObj)
    {
        var prestigeLevel = GameDataManager.instance.GetPrestigeLevel();
        gAEvent.SendProgressionEventComplete($"{progressionStatusPrefix}{prestigeLevel}");
    }
}

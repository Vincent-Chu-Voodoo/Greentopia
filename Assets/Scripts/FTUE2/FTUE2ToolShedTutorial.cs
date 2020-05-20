using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2ToolShedTutorial : MonoBehaviour
{
    public GameEvent BeforeLevel2;

    void Start()
    {
        if (GameDataManager.instance.GetPrestigeLevel() < 2)
            BeforeLevel2.Invoke(this);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEToolShedController : MonoBehaviour
{
    public int targetAtomLevel;

    public GameEvent OnEnterToolShed;
    public GameEvent OnTapSpawner;
    public GameEvent OnMerged;

    void Start()
    {
        OnEnterToolShed.Invoke(this);
    }

    public void TapSpawner()
    {
        OnTapSpawner.Invoke(this);
    }

    public void OnAtomCombined(object obj)
    {
        if ((obj as Atom).atomLevel == targetAtomLevel)
        {
            FTUEGardenPlant.haveNutrition = true;
            OnMerged.Invoke(this);
        }
    }
}

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
        var newAtom = obj as Atom;

        if (newAtom.atomLevel == targetAtomLevel)
        {
            FTUEGardenPlant.haveNutrition = true;
            OnMerged.Invoke(this);
        }

        if (newAtom.atomType == AtomEnum.fertiliser && newAtom.atomLevel == 4)
            newAtom.SetCanCraft(true);
    }
}

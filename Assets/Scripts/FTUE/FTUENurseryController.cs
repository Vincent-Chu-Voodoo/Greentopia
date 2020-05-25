using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FTUENurseryController : MonoBehaviour
{
    public int combineCount;

    public FTUEHandAnimation tUEHandAnimation;
    public AtomController atomController;

    public GameEvent OnEnterNursery;
    public GameEvent OnMergeLog;
    public GameEvent OnClickCotton;
    public GameEvent OnMergeCotton;

    void Start()
    {
        OnEnterNursery.Invoke(this);
    }

    public void AtomSpawn(object atomObj)
    {
        if (tUEHandAnimation.anchorFrom == null)
            tUEHandAnimation.anchorFrom = (atomObj as Atom).transform;
        else
            tUEHandAnimation.anchorTo = (atomObj as Atom).transform;
    }

    public void ClickCotton()
    {
        OnClickCotton.Invoke(this);
    }

    public void OnAtomCombined(object obj)
    {
        var newAtom = obj as Atom;

        if (newAtom.atomType == AtomEnum.apple_sapling && newAtom.atomLevel == 2)
        {
            newAtom.SetCanCraft(true);
            OnMergeLog.Invoke(this);
        }

        if (newAtom.atomType == AtomEnum.cotton && newAtom.atomLevel == 3)
        {
            newAtom.SetCanCraft(true);
            OnMergeCotton.Invoke(this);
        }
    }

    public void ActivateAllAtom()
    {
        foreach (var atom in atomController.allAtomList)
        {
            atom.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void StopAllAtom()
    {
        atomController.allAtomList.ForEach(i => i.GetComponent<BoxCollider>().enabled = false);
    }
}

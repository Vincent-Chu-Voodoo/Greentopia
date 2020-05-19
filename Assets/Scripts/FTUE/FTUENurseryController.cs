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
        if (atomController.allAtomList.Where(i => i.atomType == AtomEnum.apple_sapling && i.atomLevel == 2).Count() > 1)
            return;

        if (combineCount++ == 0)
            OnMergeLog.Invoke(this);

        if (atomController.allAtomList.Where(i => i.atomType == AtomEnum.cotton && i.atomLevel == 2).Count() != 1)
            return;

        if (combineCount < 10)
            OnMergeCotton.Invoke(this);
        combineCount += 10;
    }

    public void ActivateAllAtom()
    {
        foreach (var atom in atomController.allAtomList)
        {
            atom.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUENurseryController : MonoBehaviour
{
    public int combineCount;

    public AtomController atomController;

    public GameEvent OnEnterNursery;
    public GameEvent OnClickCotton;
    public GameEvent OnMergeCotton;
    public GameEvent OnMergeLog;

    void Start()
    {
        OnEnterNursery.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickCotton()
    {
        OnClickCotton.Invoke(this);
    }

    public void OnAtomCombined(object obj)
    {
        if (combineCount == 0)
            OnMergeCotton.Invoke(this);
        if (combineCount == 1)
            OnMergeLog.Invoke(this);
        combineCount++;
    }

    public void ActivateAllAtom()
    {
        foreach (var atom in atomController.allAtomList)
        {
            atom.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

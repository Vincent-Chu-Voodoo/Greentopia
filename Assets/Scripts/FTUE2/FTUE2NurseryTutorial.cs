using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2NurseryTutorial : FTUE2Tutorial
{
    //void Start()
    //{
    //    if (tutorialIndex == 38)
    //    {
    //        transform.GetChild(0).gameObject.SetActive(true);
    //    }
    //}

    protected new void Start()
    {
        if (tutorialIndex == 37)
            base.Start();
        else
            Destroy(gameObject);
    }

    public void OnAtomCombined(object atomObj)
    {
        var atom = atomObj as Atom;
        if (atom.atomType == AtomEnum.cotton && atom.atomLevel == 3)
        {
            tutorialIndex = 39;
            Proceed();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2NurseryTutorial : FTUE2Tutorial
{
    protected new void Start()
    {
        if (tutorialIndex == 37 || tutorialIndex == 46)
            base.Start();
        else
            Destroy(gameObject);
    }

    public void OnAtomCombined(object atomObj)
    {
        var atom = atomObj as Atom;
        if (atom.atomType == AtomEnum.cotton && atom.atomLevel == 4)
        {
            tutorialIndex = 39;
            Proceed();
        }
        if (atom.atomType == AtomEnum.tomato_sapling && atom.atomLevel == 4)
        {
            tutorialIndex = 47;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2CrateTutorial : FTUE2Tutorial
{
    public int claimIndex = 0;

    protected new void Start()
    {
        if (tutorialIndex == 42)
            base.Start();
        else
            Destroy(gameObject);
    }

    public void Claim()
    {
        if (++claimIndex == 2)
            Proceed(43);
    }
}

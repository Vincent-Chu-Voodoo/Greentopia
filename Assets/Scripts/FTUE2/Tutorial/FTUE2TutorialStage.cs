using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2TutorialStage : MonoBehaviour
{
    public GameEvent OnActivate;

    void Start()
    {
        OnActivate.Invoke(this);
    }
}

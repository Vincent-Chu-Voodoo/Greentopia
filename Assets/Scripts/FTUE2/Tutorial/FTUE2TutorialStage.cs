using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2TutorialStage : MonoBehaviour
{
    public GameEvent OnActivate;
    public GameEvent OnDeActivate;

    private void OnEnable()
    {
        OnActivate.Invoke(this);
    }

    private void OnDisable()
    {
        OnDeActivate.Invoke(this);
    }

    public void TryProceed()
    {
        if (gameObject.activeSelf)
        {
            transform.parent.GetComponent<FTUE2Tutorial>().Proceed();
        }
    }
}

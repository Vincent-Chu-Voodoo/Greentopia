using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEDelayedEnable : MonoBehaviour
{
    public float delay;

    public GameEvent OnEnable;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        OnEnable.Invoke(this);
    }
}

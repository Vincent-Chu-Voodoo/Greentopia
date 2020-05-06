using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUETimedEvent : MonoBehaviour
{
    public float time;

    public GameEvent OnTimesUp;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        OnTimesUp.Invoke(this);
    }
}

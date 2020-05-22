using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2TapAnywhere : MonoBehaviour
{
    public float autoTapDelay = 8f;
    public GameEvent OnTapAnyWhere;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(autoTapDelay);
        Tapped();
    }

    public void Tapped()
    {
        print($"OnTapAnyWhere");
        OnTapAnyWhere.Invoke(this);
    }
}

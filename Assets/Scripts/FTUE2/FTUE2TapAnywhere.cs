using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2TapAnywhere : MonoBehaviour
{
    public GameEvent OnTapAnyWhere;

    public void Tapped()
    {
        print($"OnTapAnyWhere");
        OnTapAnyWhere.Invoke(this);
    }
}

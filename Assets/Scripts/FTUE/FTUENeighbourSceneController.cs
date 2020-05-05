using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUENeighbourSceneController : MonoBehaviour
{
    public static bool haveApple;

    public GameEvent OnEnterNoApple;
    public GameEvent OnEnterHaveApple;
    public GameEvent OnClaim;

    void Start()
    {
        if (haveApple)
            OnEnterHaveApple.Invoke(this);
        else
            OnEnterNoApple.Invoke(this);
    }
}

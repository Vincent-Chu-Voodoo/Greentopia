using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2SmallCrates : MonoBehaviour
{
    public FTUE2BoardEnum boardEnum;

    void Start()
    {
        if (GameDataManager.instance.gameData.crateList.Exists(i => i.targetBoard == boardEnum))
        {

        }
        else
            Destroy(gameObject);
    }
}

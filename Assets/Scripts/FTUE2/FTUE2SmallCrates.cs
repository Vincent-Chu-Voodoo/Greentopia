using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2SmallCrates : MonoBehaviour
{
    public FTUE2BoardEnum boardEnum;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        gameObject.SetActive(GameDataManager.instance.gameData.crateList.Exists(i => i.targetBoard == boardEnum));
    }
}

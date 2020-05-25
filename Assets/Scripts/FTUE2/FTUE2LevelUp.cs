using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2LevelUp : MonoBehaviour
{
    public GameObject root;

    public FTUE2CrateSData crate1;
    public FTUE2CrateSData crate2;

    public List<FTUE2SmallCrates> smallCratesList;

    public void LevelUp()
    {
        root.SetActive(true);
        foreach (var sc in smallCratesList)
            sc.Refresh();
    }

    public void LevelUpHidden()
    {
        GameDataManager.instance.gameData.crateList.Add(crate1);
        GameDataManager.instance.gameData.crateList.Add(crate2);
    }

    public void Confirm()
    {
        root.SetActive(false);
    }
}

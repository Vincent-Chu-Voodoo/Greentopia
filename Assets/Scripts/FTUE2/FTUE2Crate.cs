using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2Crate : MonoBehaviour
{
    public float price;
    public FTUE2CrateSData fTUE2CrateSData;

    public GameEvent OnCliam;

    public void Claim()
    {
        if (GameDataManager.instance.gameData.coin < price)
            return;
        GameDataManager.instance.gameData.coin -= price;
        GameDataManager.instance.gameData.crateList.Add(fTUE2CrateSData);
        GetComponent<Animator>().SetTrigger("claim");
        if (price == 0f)
            price = 400f;
        OnCliam.Invoke(this);
    }
}

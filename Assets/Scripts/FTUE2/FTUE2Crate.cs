using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2Crate : MonoBehaviour
{
    public static float nurseryPrice;
    public static float toolShedPrice;
    public float price;
    public FTUE2CrateSData fTUE2CrateSData;

    public GameEvent OnCliam;

    private void Start()
    {
        switch(fTUE2CrateSData.targetBoard)
        {
            case FTUE2BoardEnum.NurseryBoard:
                price = nurseryPrice;
                break;
            case FTUE2BoardEnum.ToolShedBoard:
                price = toolShedPrice;
                break;
            default:
                throw new System.Exception();
        }
    }

    public void Claim()
    {
        if (GameDataManager.instance.gameData.coin < price)
            return;
        GameDataManager.instance.gameData.coin -= price;
        GameDataManager.instance.gameData.crateList.Add(fTUE2CrateSData);
        GetComponent<Animator>().SetTrigger("claim");
        switch (fTUE2CrateSData.targetBoard)
        {
            case FTUE2BoardEnum.NurseryBoard:
                nurseryPrice = 400f;
                break;
            case FTUE2BoardEnum.ToolShedBoard:
                toolShedPrice = 400f;
                break;
            default:
                throw new System.Exception();
        }
        switch (fTUE2CrateSData.targetBoard)
        {
            case FTUE2BoardEnum.NurseryBoard:
                price = nurseryPrice;
                break;
            case FTUE2BoardEnum.ToolShedBoard:
                price = toolShedPrice;
                break;
            default:
                throw new System.Exception();
        }
        OnCliam.Invoke(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FTUE2CrateSData", menuName = "SData/FTUE2CrateSData")]
public class FTUE2CrateSData : ScriptableObject
{
    public float purchasePrice;
    public float cooldownInSecond;
    public bool randomize;
    public AtomEnum atomEnum;
    public FTUE2BoardEnum targetBoard;
    public List<FTUE2IngredientBaseSData> fTUE2IngredientBaseSDataList;
}

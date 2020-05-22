using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "FTUE2SellableSData", menuName = "SData/FTUE2SellableSData")]
public class FTUE2SellableSData : ScriptableObject
{
    public AssetReference sellableSprite;
    public AtomEnum atomEnum;
    public float sellPrice;
}

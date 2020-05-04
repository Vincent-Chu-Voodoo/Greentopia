using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "PlantSData", menuName = "SData/PlantSData")]
public class PlantSData : ScriptableObject
{
    public string plantName;
    public float prestigePoint;
    public float prestigeLevelRequirement;
    public List<IngredientData> ingredientList;
    //public Sprite plantSprite;
    //public AssetReference plantAR;
}

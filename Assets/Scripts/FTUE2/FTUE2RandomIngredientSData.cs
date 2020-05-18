using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FTUE2RandomIngredientSData", menuName = "SData/FTUE2RandomIngredientSData")]
public class FTUE2RandomIngredientSData : FTUE2IngredientBaseSData
{
    public List<FTUE2IngredientSData> fTUE2IngredientSData;
    public FTUE2IngredientSData selectedIngredient
    {
        get
        {
            if (_selectedIngredient is null)
            {
                var ranWeight = Random.Range(0f, weight);
                var sumWeight = 0f;
                for (var i = 0; i < fTUE2IngredientSData.Count; i++)
                {
                    sumWeight += fTUE2IngredientSData[i].weight;
                    if (ranWeight <= sumWeight)
                    {
                        _selectedIngredient = fTUE2IngredientSData[i];
                        break;
                    }
                }
            }
            return _selectedIngredient;
        }
    }
    public FTUE2IngredientSData _selectedIngredient;

    public override AtomEnum atomEnum
    {
        get
        {
            return selectedIngredient.atomEnum;
        }
    }
    public override int atomLevel
    {
        get
        {
            return selectedIngredient.atomLevel;
        }
    }
    public override bool isDusty
    {
        get
        {
            return selectedIngredient.isDusty;
        }
    }
    public override float weight
    {
        get
        {
            return fTUE2IngredientSData.Sum(i => i.weight);
        }
    }
    public override void Reset()
    {
        _selectedIngredient = null;
    }
}

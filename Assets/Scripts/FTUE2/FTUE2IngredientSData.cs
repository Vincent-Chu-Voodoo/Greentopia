using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FTUE2IngredientSData", menuName = "SData/FTUE2IngredientSData")]
public class FTUE2IngredientSData : FTUE2IngredientBaseSData
{
    public AtomEnum _atomEnum;
    public int _atomLevel;
    public bool _isDusty;
    public float _weight;

    public override AtomEnum atomEnum
    {
        get
        {
            return _atomEnum;
        }
        set
        {
            _atomEnum = value;
        }
    }
    public override int atomLevel
    {
        get
        {
            return _atomLevel;
        }
        set
        {
            _atomLevel = value;
        }
    }
    public override bool isDusty
    {
        get
        {
            return _isDusty;
        }
        set
        {
            _isDusty = value;
        }
    }
    public override float weight
    {
        get
        {
            return _weight;
        }
        set
        {
            _weight = value;
        }
    }
}

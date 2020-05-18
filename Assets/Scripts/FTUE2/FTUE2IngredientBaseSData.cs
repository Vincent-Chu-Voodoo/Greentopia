using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FTUE2IngredientBaseSData", menuName = "SData/FTUE2IngredientBaseSData")]
public class FTUE2IngredientBaseSData : ScriptableObject
{
    public virtual AtomEnum atomEnum { get; set; }
    public virtual int atomLevel { get; set; }
    public virtual bool isDusty { get; set; }
    public virtual float weight { get; set; }
    public virtual void Reset()
    {

    }
}

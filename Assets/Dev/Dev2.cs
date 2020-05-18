using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev2 : MonoBehaviour
{
    public FTUE2IngredientBaseSData fTUE2IngredientBaseSData;

    [ContextMenu("Check")]
    public void Check()
    {
        print($"atomEnum: {fTUE2IngredientBaseSData.atomEnum}");
        print($"level: {fTUE2IngredientBaseSData.atomLevel}");
        print($"isDusty: {fTUE2IngredientBaseSData.isDusty}");
    }
}

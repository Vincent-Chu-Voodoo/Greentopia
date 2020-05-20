using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev2 : MonoBehaviour
{
    public FTUE2IngredientBaseSData fTUE2IngredientBaseSData;

    [ContextMenu("Check")]
    public void Check()
    {
        print($"position: {transform.position}");
    }
}

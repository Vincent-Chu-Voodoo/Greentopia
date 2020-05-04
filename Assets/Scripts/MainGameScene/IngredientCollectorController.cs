using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Obsolete("obsoleted")]
public class IngredientCollectorController : MonoBehaviour
{
    //[Header("Display")]
    //public List<IngredientCollectorPanel> ingredientCollectorPanelList;

    //[Header("Config")]
    //public AssetReference collectorPanelAR;
    //public AtomController atomController;

    //public void Setup(List<IngredientCollector> ingredientCollectorList)
    //{
    //    foreach (var ingredientCollector in ingredientCollectorList)
    //    {
    //        var aoh = collectorPanelAR.InstantiateAsync(transform);
    //        aoh.Completed += _ =>
    //        {
    //            var ingredientCollectorPanel = aoh.Result.GetComponent<IngredientCollectorPanel>();
    //            ingredientCollectorPanel.Setup(ingredientCollector);
    //            ingredientCollectorPanelList.Add(ingredientCollectorPanel);
    //        };
    //    }
    //}

    //public List<IngredientData> QuitCollect()
    //{
    //    var newCollectedIngredientList = new List<IngredientData>();
    //    foreach (var ingredientCollectorPanel in ingredientCollectorPanelList)
    //    {
    //        if (ingredientCollectorPanel.collectionCount > 0f)
    //            newCollectedIngredientList.Add(
    //                new IngredientData()
    //                {
    //                    atomEnum = ingredientCollectorPanel.ingredientCollector.collectionType,
    //                    level = ingredientCollectorPanel.ingredientCollector.collectionLevel,
    //                    count = ingredientCollectorPanel.collectionCount,
    //                }
    //            );
    //    }
    //    return newCollectedIngredientList.Count > 0 ? newCollectedIngredientList : null;
    //}

    //public void AtomCombined(object atomObj)
    //{
    //    AtomCombined(atomObj as Atom);
    //}

    //public void AtomCombined(Atom atom)
    //{
    //    var ingredientCollectorPanel = ingredientCollectorPanelList.Where(i => i.ingredientCollector.collectionType == atom.atomType && i.ingredientCollector.collectionLevel == atom.atomLevel).FirstOrDefault();
    //    if (ingredientCollectorPanel)
    //        ingredientCollectorPanel.Collect(atom, atomController);
    //}
}

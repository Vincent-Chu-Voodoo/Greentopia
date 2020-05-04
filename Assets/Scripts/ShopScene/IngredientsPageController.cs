using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class IngredientsPageController : MonoBehaviour
{
    [Header("Display")]
    public List<IngredientData> ingredientList;

    [Header("Config")]
    public AssetReference ingredientsPanelAR;
    public Transform ingredientsPanelRoot;

    protected void OnEnable()
    {
        SoftRefresh();
    }

    public void SoftRefresh()
    {
        if (ingredientList == null || ingredientList.Count == 0)
            HardRefersh();
    }

    public void HardRefersh()
    {
        var ingredientList = GameDataManager.instance.GenerateIngredientList();

        foreach (Transform trans in ingredientsPanelRoot)
            Addressables.ReleaseInstance(trans.gameObject);

        foreach (var ingredient in ingredientList)
        {
            var aoh = ingredientsPanelAR.InstantiateAsync(ingredientsPanelRoot);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<IngredientsPanel>().Setup(ingredient);
            };
        }
    }
}

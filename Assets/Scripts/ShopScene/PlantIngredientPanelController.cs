
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlantIngredientPanelController : MonoBehaviour
{
    [Header("Display")]
    public bool isAllIngredientSatisfied;
    public List<PlantIngredientPanel> plantIngredientPanelList;

    [Header("Config")]
    public AssetReference plantIngredientPanelAR;
    public PlantPanelController plantPanelController;

    [Header("Event")]
    public GameEvent OnIngredientSatisfiedUpdate;

    public void Setup(List<IngredientData> ingredientList)
    {
        var count = ingredientList.Count();
        foreach (var ingredent in ingredientList)
        {
            var aoh = plantIngredientPanelAR.InstantiateAsync(transform);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<PlantIngredientPanel>().Setup(ingredent);
                plantIngredientPanelList.Add(aoh.Result.GetComponent<PlantIngredientPanel>());
                if (--count == 0)
                    RefreshSatisfication();
            };
        }
    }

    public void RefreshSatisfication()
    {
        isAllIngredientSatisfied = plantIngredientPanelList.Where(i => !i.isIngredientSatisfied).Count() == 0;
        OnIngredientSatisfiedUpdate.Invoke(isAllIngredientSatisfied);
        //plantPanelController.IngredientSatisfied(isAllIngredientSatisfied);
    }

    public void RefreshSatisfication(List<IngredientData> newIngredientData)
    {
        foreach (var plantIngredientPanel in plantIngredientPanelList)
        {
            var ingredientHave = newIngredientData.Find(i => i.atomEnum == plantIngredientPanel.ingredientData.atomEnum && Mathf.Abs(i.level - plantIngredientPanel.ingredientData.level) < 0.1f);
            plantIngredientPanel.RefreshRequirement(ingredientHave?.count ?? 0f);
        }
        RefreshSatisfication();
    }
}

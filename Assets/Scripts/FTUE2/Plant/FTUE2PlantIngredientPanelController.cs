using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2PlantIngredientPanelController : MonoBehaviour
{
    [Header("Display")]
    public bool isAllIngredientSatisfied;
    public List<FTUE2PlantIngredientPanel> plantIngredientPanelList;

    [Header("Config")]
    public AssetReference plantIngredientPanelAR;

    [Header("Event")]
    public GameEvent OnIngredientSatisfiedUpdate;

    public void Setup(List<IngredientData> ingredientList)
    {
        var count = ingredientList.Count();
        while (plantIngredientPanelList.Count > 0)
        {
            Destroy(plantIngredientPanelList[0].gameObject);
            plantIngredientPanelList.RemoveAt(0);
        }
        foreach (var ingredent in ingredientList)
        {
            var aoh = plantIngredientPanelAR.InstantiateAsync(transform);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<FTUE2PlantIngredientPanel>().Setup(ingredent);
                plantIngredientPanelList.Add(aoh.Result.GetComponent<FTUE2PlantIngredientPanel>());
                if (--count == 0)
                    RefreshSatisfication();
            };
        }
    }

    public void UpdateIngredient(List<IngredientData> ingredientDataList)
    {
        foreach (var plantIngredientPanel in plantIngredientPanelList)
        {
            var ingredientHave = ingredientDataList.Find(i => i.atomEnum == plantIngredientPanel.ingredientData.atomEnum && Mathf.Abs(i.level - plantIngredientPanel.ingredientData.level) < 0.1f);
            plantIngredientPanel.RefreshRequirement(ingredientHave?.count ?? 0f);
        }
        //RefreshSatisfication();
    }

    public void RefreshSatisfication()
    {
        UpdateIngredient(GameDataManager.instance.GenerateIngredientList());
        isAllIngredientSatisfied = plantIngredientPanelList.Where(i => !i.isIngredientSatisfied).Count() == 0;
        OnIngredientSatisfiedUpdate.Invoke(isAllIngredientSatisfied);
    }

    //public void RefreshSatisfication(List<IngredientData> newIngredientData)
    //{
    //    foreach (var plantIngredientPanel in plantIngredientPanelList)
    //    {
    //        var ingredientHave = newIngredientData.Find(i => i.atomEnum == plantIngredientPanel.ingredientData.atomEnum && Mathf.Abs(i.level - plantIngredientPanel.ingredientData.level) < 0.1f);
    //        plantIngredientPanel.RefreshRequirement(ingredientHave?.count ?? 0f);
    //    }
    //    RefreshSatisfication();
    //}
}

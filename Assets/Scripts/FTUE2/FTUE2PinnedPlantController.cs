using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PinnedPlantController : MonoBehaviour
{
    public FTUE2Plant fTUE2Plant;
    public FTUE2PlantController fTUE2PlantController;
    public PlantSData pinnedPlant;
    public FTUE2PlantPanel fTUE2PlantPanel;

    private void Start()
    {
        PinPlant();
    }

    public void PinPlant()
    {
        gameObject.SetActive(true);
        pinnedPlant = GameDataManager.instance.gameData.pinnedPlant;
        if (pinnedPlant is null)
        {
            gameObject.SetActive(false);
            return;
        }
        var count = GameDataManager.instance.gameData.gardentPlantList.Find(i => i.plantData.plantName == pinnedPlant.plantName)?.plantData.count ?? 0;
        PinPlant(pinnedPlant, count);
        fTUE2PlantPanel.OnSatisfiedUpdate.AddListener(OnSatisfiedUpdate);
    }

    public void OnSatisfiedUpdate(object obj)
    {
        if (fTUE2PlantPanel.plantIngredientPanelController.isAllIngredientSatisfied)
        {
            ActivatePlant();
        }
    }

    public void ActivatePlant()
    {
        if (fTUE2Plant is null)
        {

        }
    }

    public void PinPlant(PlantSData plantSData, int count = 0)
    {
        fTUE2PlantPanel.Setup(plantSData, count, true);
    }

    public void OnAtomCombined(object atomObj)
    {
        RefreshIngredient();
    }

    public void RefreshIngredient()
    {
        fTUE2PlantPanel.RefreshIngredient();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PinnedPlantController : MonoBehaviour
{
    public FTUE2Plant fTUE2Plant;
    public FTUE2PlantController fTUE2PlantController;
    public PlantSData pinnedPlant;
    public FTUE2PlantPanel fTUE2PlantPanel;
    public Transform plantSpawnAnchor;

    public GameEvent OnPlannted;

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
        print($"ActivatePlant");
        if (fTUE2Plant is null && !(fTUE2PlantController is null))
        {
            fTUE2Plant = fTUE2PlantController.SpawnPlant(pinnedPlant, 1);
            fTUE2Plant.transform.position = plantSpawnAnchor.position;
            fTUE2Plant.OnPlanted.AddListener(Plannted);
        }
    }

    public void Plannted(object obj)
    {
        fTUE2Plant = null;
        GameDataManager.instance.gameData.pinnedPlant = null;
        gameObject.SetActive(false);
        OnPlannted.Invoke(this);
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

    private void OnEnable()
    {
        fTUE2Plant?.gameObject?.SetActive(true);
    }

    private void OnDisable()
    {
        fTUE2Plant?.gameObject?.SetActive(false);
    }
}

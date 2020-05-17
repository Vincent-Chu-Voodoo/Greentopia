using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PinnedPlantController : MonoBehaviour
{
    public FTUE2PlantPanel fTUE2PlantPanel;

    private void Start()
    {
        PinPlant();
    }

    public void PinPlant()
    {
        gameObject.SetActive(true);
        var pinnedPlant = GameDataManager.instance.gameData.pinnedPlant;
        if (pinnedPlant is null)
        {
            gameObject.SetActive(false);
            return;
        }
        var count = GameDataManager.instance.gameData.gardentPlantList.Find(i => i.plantData.plantName == pinnedPlant.plantName)?.plantData.count ?? 0;
        PinPlant(pinnedPlant, count);
    }

    public void PinPlant(PlantSData plantSData, int count = 0)
    {
        fTUE2PlantPanel.Setup(plantSData, count, true);
    }
}

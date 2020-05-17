using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FTUE2HerbariumController : MonoBehaviour
{
    [Header("Display")]
    public List<FTUE2PlantPanel> plantPanelList;

    [Header("Config")]
    public FTUE2PinnedPlantController fTUE2PinnedPlantController;
    public Transform mainGamePlantPanelRoot;
    public AssetReference mainGamePlantPanelAR;
    public ScrollTo scrollTo;

    private void Awake()
    {
        foreach (var plantSData in GameDataManager.instance.plantSDataList)
        {
            if (GameDataManager.instance.GetPrestigeLevel() < plantSData.prestigeLevelRequirement)
                continue;
            var aoh = mainGamePlantPanelAR.InstantiateAsync(mainGamePlantPanelRoot);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<FTUE2PlantPanel>().Setup(plantSData);
                aoh.Result.GetComponent<FTUE2PlantPanel>().OnPin.AddListener(Pin);
                plantPanelList.Add(aoh.Result.GetComponent<FTUE2PlantPanel>());
            };
        }
    }

    public void Pin(object fTUE2PlatPanelObj)
    {
        var fTUE2PlatPanel = fTUE2PlatPanelObj as FTUE2PlantPanel;
        GameDataManager.instance.gameData.pinnedPlant = fTUE2PlatPanel.plantSData;
        fTUE2PinnedPlantController.PinPlant();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if (gameObject.activeSelf)
            Hide();
        else
            Show();
    }
}

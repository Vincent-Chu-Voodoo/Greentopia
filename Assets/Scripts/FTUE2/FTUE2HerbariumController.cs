using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public class FTUE2HerbariumController : MonoBehaviour
{
    [Header("Display")]
    public int currentDisplayIndex;
    public List<FTUE2PlantPanel> plantPanelList;

    [Header("Config")]
    public FTUE2PinnedPlantController fTUE2PinnedPlantController;
    public Transform mainGamePlantPanelRoot;
    public AssetReference mainGamePlantPanelAR;
    public ScrollTo scrollTo;
    public GameObject leftGO;
    public GameObject rightGO;

    public GameEvent OnPin;

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
                ShowIndex(currentDisplayIndex);
            };
        }
        GetComponent<Animator>().Play("close", 0, 1f);
    }

    public void Pin(object fTUE2PlatPanelObj)
    {
        var fTUE2PlatPanel = fTUE2PlatPanelObj as FTUE2PlantPanel;
        GameDataManager.instance.gameData.pinnedPlant = fTUE2PlatPanel.plantSData;
        fTUE2PinnedPlantController.PinPlant();
        Hide();
        OnPin.Invoke(this);
    }

    public void Show()
    {
        //gameObject.SetActive(true);
        print($"show");
        fTUE2PinnedPlantController.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("isOpen", true);
        foreach (var plantPanel in plantPanelList)
            plantPanel.pinGO.SetActive(plantPanel.plantSData.plantName != (GameDataManager.instance.gameData.pinnedPlant?.plantName ?? ""));
    }

    public void Hide()
    {
        fTUE2PinnedPlantController.gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("isOpen", false);
        //gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if (GetComponent<Animator>().GetBool("isOpen"))
            Hide();
        else
            Show();
    }

    public void StopPinningAppleTree()
    {
        StartCoroutine(StopWithDelay());
    }

    IEnumerator StopWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        plantPanelList[0].pinGO.GetComponent<EventTrigger>().enabled = false;
    }

    public void GoLeft()
    {
        currentDisplayIndex = Mathf.Clamp(currentDisplayIndex - 1, 0, plantPanelList.Count - 1);
        ShowIndex(currentDisplayIndex);
    }

    public void GoRight()
    {
        currentDisplayIndex = Mathf.Clamp(currentDisplayIndex + 1, 0, plantPanelList.Count - 1);
        ShowIndex(currentDisplayIndex);
    }

    public void ShowIndex(int index)
    {
        plantPanelList.ForEach(i => i.gameObject.SetActive(false));
        plantPanelList[index].gameObject.SetActive(true);
        leftGO.SetActive(index != 0);
        rightGO.SetActive(index != plantPanelList.Count - 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PrestigeDescriptionController : MonoBehaviour
{
    [Header("Config")]
    public AssetReference plantPreviewAR;
    public Transform plantPreviewRoot;
    public TextMeshProUGUI prestigePointLeftText;

    public void Start()
    {
        var currentPrestigeLevel = GameDataManager.instance.GetPrestigeLevel();
        var prestigeNeededToLevelUp = GameDataManager.instance.GetPrestigeNeededForNextLevel();
        var targetPlantSDataList = GameDataManager.instance.plantSDataList.Where(i => i.prestigeLevelRequirement == currentPrestigeLevel + 1);
        foreach (var targetPlantSData in targetPlantSDataList)
        {
            var aoh = plantPreviewAR.InstantiateAsync(plantPreviewRoot);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<PlantPreview>().Setup(targetPlantSData);
            };
        }
        prestigePointLeftText.SetText(prestigePointLeftText.text, prestigeNeededToLevelUp);

        if (GameDataManager.instance.gameData.tutorialData.iKnowWhatIsPrestige)
        {
            gameObject.SetActive(false);
            return;
        }
        GameDataManager.instance.gameData.tutorialData.iKnowWhatIsPrestige = true;
    }
}

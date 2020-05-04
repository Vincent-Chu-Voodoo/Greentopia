using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    [Header("Config")]
    public GameObject rootGo;
    public YouUnlocked youUnlocked;
    public TextMeshProUGUI levelText;

    [Header("Event")]
    public GameEvent OnBegin;
    public GameEvent OnEnd;

    public void LevelUp(object prestigePointControllerObj)
    {
        LevelUp(prestigePointControllerObj as PrestigePointController);
    }

    public void LevelUp(PrestigePointController prestigePointController)
    {
        rootGo.SetActive(true);
        var prestigeLevel = GameDataManager.instance.GetPrestigeLevel();
        var targetPlantSDataList = GameDataManager.instance.plantSDataList.Where(i => i.prestigeLevelRequirement == prestigeLevel).ToList();
        var targetLevelSDataList = GameDataManager.instance.levelSDataList.Where(i => i.prestigeLevel == prestigeLevel).ToList();

        youUnlocked.Setup(targetPlantSDataList, targetLevelSDataList);
        levelText.SetText(string.Format(levelText.text, prestigeLevel));
        OnBegin.Invoke(this);
    }

    public void Continue()
    {
        rootGo.SetActive(false);
        OnEnd.Invoke(this);
    }
}

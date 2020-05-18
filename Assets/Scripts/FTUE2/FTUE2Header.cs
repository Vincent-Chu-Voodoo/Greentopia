using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FTUE2Header : MonoBehaviour
{
    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI diamondCount;
    public TextMeshProUGUI xpCount;
    public Image xpFiller;

    public GameEvent OnLevelUp;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        coinCount.SetText($"{GameDataManager.instance.gameData?.coin}");
        diamondCount.SetText($"{GameDataManager.instance.gameData?.diamond}");
        xpCount.SetText($"{GameDataManager.instance.GetPrestigeNeededForNextLevel()} more");
        var percent = GameDataManager.instance.GetLevelPercent();
        xpFiller.fillAmount = percent;
    }

    public void AddXp(float newXp)
    {
        var currentLevel = GameDataManager.instance.GetPrestigeLevel();
        GameDataManager.instance.gameData.prestigePoint += newXp;
        var newLevel = GameDataManager.instance.GetPrestigeLevel();
        if (newLevel > currentLevel)
            OnLevelUp.Invoke(this);
        Refresh();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FTUE2Header : MonoBehaviour
{
    private float increaseSpeed = 65f;

    public float addXpDelay;

    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI diamondCount;
    public TextMeshProUGUI xpCount;
    public TextMeshProUGUI lvTMP;
    public Image xpFiller;

    public GameEvent OnLevelUp;
    public GameEvent OnLevelUpHidden;

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
        lvTMP.SetText($"{GameDataManager.instance.GetPrestigeLevel()}");
    }

    public void Refresh(float xp)
    {
        coinCount.SetText($"{GameDataManager.instance.gameData?.coin}");
        diamondCount.SetText($"{GameDataManager.instance.gameData?.diamond}");
        xpCount.SetText($"{GameDataManager.instance.GetPrestigeNeededForNextLevel(xp):0} more");
        var percent = GameDataManager.instance.GetLevelPercentFromXp(xp);
        xpFiller.fillAmount = percent;
        lvTMP.SetText($"{GameDataManager.instance.GetPrestigeLevel(xp):0}");
    }

    public void AddXp(float newXp)
    {
        var currentXp = GameDataManager.instance.gameData.prestigePoint;
        var currentLevel = GameDataManager.instance.GetPrestigeLevel(currentXp);
        var newLevel = GameDataManager.instance.GetPrestigeLevel(currentXp + newXp);
        if (newLevel > currentLevel)
        {
            OnLevelUpHidden.Invoke(this);
        }
        StartCoroutine(MoveToNewXp(GameDataManager.instance.gameData.prestigePoint + newXp));
    }

    public void AddCoin(float newCoin)
    {
        GameDataManager.instance.gameData.coin += newCoin;
        Refresh();
    }

    public void AddDiamond(float newDiamond)
    {
        GameDataManager.instance.gameData.diamond += newDiamond;
        Refresh();
    }

    public IEnumerator MoveToNewXp(float targetXp)
    {
        yield return new WaitForSeconds(addXpDelay);
        GetComponent<Animator>().SetBool("adding_xp", true);
        var currentXp = GameDataManager.instance.gameData.prestigePoint;
        GameDataManager.instance.gameData.prestigePoint = targetXp;
        while (currentXp < targetXp)
        {
            var currentLevel = GameDataManager.instance.GetPrestigeLevel(currentXp);
            currentXp = Mathf.MoveTowards(currentXp, targetXp, increaseSpeed * Time.deltaTime);
            Refresh(currentXp);
            var newLevel = GameDataManager.instance.GetPrestigeLevel(currentXp);
            if (newLevel > currentLevel)
            {
                OnLevelUp.Invoke(this);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        GetComponent<Animator>().SetBool("adding_xp", false);
        Refresh();
    }
}

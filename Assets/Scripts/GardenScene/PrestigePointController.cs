using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigePointController : MonoBehaviour
{
    [Header("Display")]
    public float currentLevelF;
    public float targetLevelF;

    [Header("Param")]
    public float speed;

    [Header("Config")]
    public GameObject descriptionGameObject;
    public PrestigePointSData prestigePointSData;
    public TextMeshProUGUI prestigeLevelText;
    public TextMeshProUGUI prestigePointText;
    public Image prestigeBarImage;

    [Header("Event")]
    public GameEvent OnLevelUp;

    public void ShowDescription()
    {
        descriptionGameObject.SetActive(true);
    }

    public void HideDescription()
    {
        descriptionGameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateCurrentLevel(Mathf.MoveTowards(currentLevelF, targetLevelF, speed * Time.deltaTime));
    }

    public void LerpToPrestigePoint(float accummulatedPrestigePoint)
    {
        targetLevelF = GameDataManager.instance.GetLevelF(accummulatedPrestigePoint);
        var prestigeLeft = GameDataManager.instance.GetPrestigeNeededForNextLevel(); //(GameDataManager.instance.GetNextLevelAccumulatedPrestigePoint(accummulatedPrestigePoint) - GameDataManager.instance.GetCurrentLevelAccumulatedPrestigePoint(accummulatedPrestigePoint)) - (accummulatedPrestigePoint - GameDataManager.instance.GetCurrentLevelAccumulatedPrestigePoint(accummulatedPrestigePoint));
        prestigePointText.SetText($"Earn {prestigeLeft:0} Prestige");
    }

    public void ToPrestigePoint(float accummulatedPrestigePoint)
    {
        var levelF = GameDataManager.instance.GetLevelF(accummulatedPrestigePoint);
        currentLevelF = levelF;
        targetLevelF = levelF;
        //GameDataManager.instance.tempPrestigeLevel = GetLevel(currentLevelF);
        prestigeLevelText.SetText($"{GameDataManager.instance.GetLevel(currentLevelF)}");
        prestigeBarImage.fillAmount = GameDataManager.instance.GetLevelPercent(currentLevelF);
        var prestigeLeft = GameDataManager.instance.GetPrestigeNeededForNextLevel(); //(GetNextLevelAccumulatedPrestigePoint(accummulatedPrestigePoint) - GetCurrentLevelAccumulatedPrestigePoint(accummulatedPrestigePoint)) - (accummulatedPrestigePoint - GetCurrentLevelAccumulatedPrestigePoint(accummulatedPrestigePoint));
        prestigePointText.SetText($"Earn {prestigeLeft:0} Prestige");
    }

    public void UpdateCurrentLevel(float newLevelF)
    {
        if (GameDataManager.instance.GetLevel(newLevelF) > GameDataManager.instance.GetLevel(currentLevelF))
        {
            OnLevelUp.Invoke(this);
            //GameDataManager.instance.tempPrestigeLevel = GameDataManager.instance.GetLevel(newLevelF);
            prestigeLevelText.SetText($"{GameDataManager.instance.GetLevel(newLevelF)}");
        }
        currentLevelF = newLevelF;
        prestigeBarImage.fillAmount = GameDataManager.instance.GetLevelPercent(currentLevelF);
    }
}

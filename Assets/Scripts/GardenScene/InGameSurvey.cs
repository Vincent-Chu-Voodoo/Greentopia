using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSurvey : MonoBehaviour
{
    [Header("Param")]
    public string surveyUrl;
    public int targetLevel;

    [Header("Config")]
    public GameObject surveyGO;

    public void Cancel()
    {
        Close();
    }

    public void Sure()
    {
        Application.OpenURL(surveyUrl);
        Close();
    }

    public void Levelup()
    {
        if (GameDataManager.instance.GetPrestigeLevel() == targetLevel)
            Open();
    }

    public void Open()
    {
        surveyGO.SetActive(true);
    }

    public void Close()
    {
        surveyGO.SetActive(false);
    }
}

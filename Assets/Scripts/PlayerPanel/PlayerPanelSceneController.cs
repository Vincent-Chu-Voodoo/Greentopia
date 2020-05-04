using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class PlayerPanelSceneParam
{
    public int prestigeLevel;
}

public class PlayerPanelSceneController : MonoBehaviour
{
    [Header("Config")]
    public AssetReference closeTargetSceneAR;
    public AssetReference playTargetSceneAR;
    public List<AtomDisplaySData> atomDisplaySDataList;
    public AssetReference levelPanelAR;
    public Transform levelPanelRoot;
    public SceneTransition sceneTransition;

    [Header("Event")]
    public GameEvent OnPlay;

    void Start()
    {
        if (GameDataManager.instance.sceneParam is PlayerPanelSceneParam)
            ProcessPlayerPanelSceneParam(GameDataManager.instance.sceneParam as PlayerPanelSceneParam);
        else
            throw new Exception("Please enter the scene from GardenScene");
    }

    public void ProcessPlayerPanelSceneParam(PlayerPanelSceneParam playerPanelSceneParam)
    {
        foreach (var levelData in GameDataManager.instance.levelSDataList)
        {
            var aoh = levelPanelAR.InstantiateAsync(levelPanelRoot);
            aoh.Completed += _ =>
            {
                var levelPanel = aoh.Result;
                levelPanel.GetComponent<LevelPanel>().Setup(levelData, atomDisplaySDataList, playerPanelSceneParam.prestigeLevel);
                levelPanel.GetComponent<LevelPanel>().OnClickPlay.AddListener(Play);
            };
        }
    }

    public void Play(object levelPanelObj)
    {
        Play(levelPanelObj as LevelPanel);
    }

    public void Play(LevelPanel levelPanel)
    {
        GameDataManager.instance.sceneParam = new MainGameParam()
        {
            levelSData = levelPanel.levelSData
        };
        sceneTransition.FadeIn(playTargetSceneAR);
        OnPlay.Invoke(this);
    }

    public void Close()
    {
        sceneTransition.FadeIn(closeTargetSceneAR);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class MainGameParam
{
    public LevelSData levelSData;
}

public class MainGameController : MonoBehaviour
{
    [Header("Display")]
    //public MainGameParam mainGameParam;
    public LevelSessionData levelSessionData;

    [Header("Param")]
    public bool useSavedGameSession;
    public FTUE2BoardEnum boardEnum;
    //public VictoryConditionSData victoryCondition;

    [Header("Config")]
    public TextMeshProUGUI levelText;
    public AssetReference closeTargetSceneAR;
    public AssetReference craftTargetSceneAR;
    public AtomController atomController;
    public MainGrid mainGrid;
    public AtomSpawner atomSpawner;
    public SceneTransition sceneTransition;

    [Header("Event")]
    public GameEvent OnInitialized;

    protected void Start()
    {
        Init();
    }

    protected void OnApplicationQuit()
    {
        QuitProcess();
    }

    public void Init()
    {
        //if (GameDataManager.instance.sceneParam is MainGameParam)
        //    mainGameParam = GameDataManager.instance.sceneParam as MainGameParam;
        GameDataManager.instance.sceneParam = null;
        //levelText.SetText($"BOARD {mainGameParam.levelSData.level}");
        InitiateGrids();
        InitiateGameSession();
        InitiateIngredientCollector();
        OnInitialized.Invoke(this);
    }

    public void InitiateGrids()
    {
        //mainGrid.rowColumn = new Vector2(mainGameParam.levelSData.row, mainGameParam.levelSData.column);
        //mainGrid.SpawnGrids();
    }

    public void InitiateGameSession()
    {
        if (useSavedGameSession)
        {
            switch (boardEnum)
            {
                case FTUE2BoardEnum.NurseryBoard:
                    if (!(GameDataManager.instance.gameData.nurserySessionData is null))
                        levelSessionData = GameDataManager.instance.gameData.nurserySessionData;
                    break;
                case FTUE2BoardEnum.ToolShedBoard:
                    if (!(GameDataManager.instance.gameData.toolShedSessionData is null))
                        levelSessionData = GameDataManager.instance.gameData.toolShedSessionData;
                    break;
                default:
                    break;
            }
        }
        //if (ReferenceEquals(levelSessionData.gridDataList, null))
        //    levelSessionData = new LevelSessionData(mainGameParam.levelSData);
        InitiateGameSession(levelSessionData);
    }

    public void InitiateIngredientCollector()
    {
        //ingredientCollectorController.Setup(mainGameParam.levelSData.ingredientCollectorList);
    }

    public void InitiateGameSession(LevelSessionData _levelSessionData)
    {
        for (var i = 0; i < _levelSessionData.gridDataList.Count; i++)
        {
            var gridData = _levelSessionData.gridDataList[i];
            atomSpawner.SpawnAtom(gridData);
        }
    }

    public void SpawnNewAtom()
    {
        //CheckVictory();
    }

    public void AtomCombine(object atomObj)
    {
        AtomCombine(atomObj as Atom);
    }

    public void AtomCombine(Atom atom)
    {
        //CheckVictory();
    }

    [Obsolete("No victory condition")]
    public void CheckVictory()
    {
        //foreach (var victoryCondition in victoryCondition.victoryConditionList)
        //{
        //    var count = atomController.CountAtom(victoryCondition.type, victoryCondition.level - 1);
        //    if (count < victoryCondition.howManyNeeded)
        //        return;
        //}
        //OnWin.Invoke(null);
        //Time.timeScale = 0f;
    }

    public void Close()
    {
        QuitProcess();
        //var newCollectedIngredientList = ingredientCollectorController.QuitCollect();
        //GameDataManager.instance.sceneParam = new GardenSceneParam() { newCollectedIngredientList = newCollectedIngredientList };
        sceneTransition.FadeIn(closeTargetSceneAR);
    }

    public void Craft(object plantSDataObj)
    {
        Craft(plantSDataObj as PlantSData);
    }

    public void Craft(PlantSData _plantSData)
    {
        GameDataManager.instance.sceneParam = new CraftSceneParam()
        {
            plantSData = _plantSData
        };
        QuitProcess();
        sceneTransition.FadeIn(craftTargetSceneAR);
        //Addressables.LoadSceneAsync(craftTargetSceneAR);
    }

    public void QuitProcess()
    {
        levelSessionData = atomController.GetLevelSessionData(0);
        GameDataManager.instance.SaveSession(levelSessionData, boardEnum);
        //GameDataManager.instance.SaveSession(levelSessionData);
    }
}

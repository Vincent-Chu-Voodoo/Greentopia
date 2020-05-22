using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Game rule dependent data manager
/// </summary>
public class GameDataManager : Singleton<GameDataManager>
{
    public GameData gameData;
    public object sceneParam;

    #region BootstrapData
    public PrestigePointSData prestigePointSData;
    public List<PlantSData> plantSDataList = new List<PlantSData>();
    public List<LevelSData> levelSDataList = new List<LevelSData>();
    #endregion

    #region MonoBehaviour
    protected void Awake()
    {
        print($"GameDataManager Awake {Application.persistentDataPath}");
        try
        {
            gameData = DataManager.instance.Load<GameData>();
        } 
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
        }
        if (gameData == null)
            gameData = new GameData();
        //GenerateIngredientList();
    }

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID)
    protected void OnApplicationPause()
    {
        print($"GameDataManager OnApplicationPause");
        DataManager.instance.Save(gameData);
    }
#else
    protected void OnApplicationQuit()
    {
        print($"GameDataManager OnApplicationQuit");
        DataManager.instance.Save(gameData);
    }
#endif
    #endregion

    [ContextMenu("ClearAllData")]
    public void ClearAllData()
    {
        gameData = new GameData();
        DataManager.instance.Save(gameData);
    }

    public PlantSData GetPlantSData(string plantName)
    {
        return plantSDataList.Find(i => string.Equals(i.plantName, plantName));
    }

    #region PrestigePointSData
    public int GetPrestigeLevel()
    {
        return GetLevel(GetLevelF(gameData.prestigePoint));
    }

    public float GetPrestigeLevelPercent()
    {
        return GetLevel(GetLevelF(gameData.prestigePoint));
    }

    public float GetCurrentLevelPrestigeMargin()
    {
        var accummulatedPrestigePoint = gameData.prestigePoint;
        return accummulatedPrestigePoint - prestigePointSData.accumulatedPrestigePointList.FindLast(i => i <= accummulatedPrestigePoint);
    }

    public float GetCurrentLevelPrestigeRequirement()
    {
        var accummulatedPrestigePoint = gameData.prestigePoint;
        return prestigePointSData.accumulatedPrestigePointList.Find(i => i > accummulatedPrestigePoint) - prestigePointSData.accumulatedPrestigePointList.FindLast(i => i < accummulatedPrestigePoint); 
    }

    public float GetPrestigeNeededForNextLevel()
    {
        var clpr = GetCurrentLevelPrestigeRequirement();
        var clpm = GetCurrentLevelPrestigeMargin();
        return clpr - clpm;
    }

    public float GetLevelPercent()
    {
        return GetLevelPercent(GetLevelF(gameData.prestigePoint));
    }

    public float GetLevelPercent(float levelF)
    {
        return levelF % 1f;
    }

    public int GetLevel(float levelF)
    {
        return Mathf.RoundToInt(levelF - GetLevelPercent(levelF));
    }

    public float GetLevelF(float accummulatedPrestigePoint)
    {
        var index = prestigePointSData.accumulatedPrestigePointList.FindIndex(i => i > accummulatedPrestigePoint);
        var level = index - 1;
        var percent = (accummulatedPrestigePoint - prestigePointSData.accumulatedPrestigePointList[level]) / (prestigePointSData.accumulatedPrestigePointList[level + 1] - prestigePointSData.accumulatedPrestigePointList[level]);
        return level + percent;
    }

    public float GetNextLevelAccumulatedPrestigePoint(float accummulatedPrestigePoint)
    {
        return prestigePointSData.accumulatedPrestigePointList.Find(i => i > accummulatedPrestigePoint);
    }

    public float GetCurrentLevelAccumulatedPrestigePoint(float accummulatedPrestigePoint)
    {
        return prestigePointSData.accumulatedPrestigePointList.FindLast(i => i < accummulatedPrestigePoint);
    }
    #endregion

    public void SaveSession(LevelSessionData levelSessionData, FTUE2BoardEnum fTUE2BoardEnum)
    {
        switch (fTUE2BoardEnum)
        {
            case FTUE2BoardEnum.NurseryBoard:
                gameData.nurserySessionData = levelSessionData;
                break;
            case FTUE2BoardEnum.ToolShedBoard:
                gameData.toolShedSessionData = levelSessionData;
                break;
            default:
                break;
        }

    }

    public void UpdateSession(LevelSessionData levelSessionData)
    {
        var index = gameData.levelSessionDataList.FindIndex(i => i.level == levelSessionData.level);
        if (index < 0)
        {
            Debug.LogWarning($"level session data not found for lv: {levelSessionData.level}");
            gameData.levelSessionDataList.Add(levelSessionData);
        }
        else
            gameData.levelSessionDataList[index] = levelSessionData;
    }

    public List<IngredientData> GenerateIngredientList()
    {
        var resultIngredientDataList = new List<IngredientData>();
        for (var i = 0; i < gameData.nurserySessionData.gridDataList.Count; i++)
        {
            var gridData = gameData.nurserySessionData.gridDataList[i];
            if (gridData.isDusty || gridData.isCrate)
                continue;
            var index = resultIngredientDataList.FindIndex(k => k.atomEnum == gridData.atomEnum && Mathf.Abs(k.level - gridData.atomLevel) < 0.1f);
            if (index < 0)
            {
                resultIngredientDataList.Add(new IngredientData()
                {
                    atomEnum = gridData.atomEnum,
                    level = gridData.atomLevel,
                    count = 1
                });
            }
            else
                resultIngredientDataList[index].count++;
        }
        for (var i = 0; i < gameData.toolShedSessionData.gridDataList.Count; i++)
        {
            var gridData = gameData.toolShedSessionData.gridDataList[i];
            if (gridData.isDusty || gridData.isCrate)
                continue;
            var index = resultIngredientDataList.FindIndex(k => k.atomEnum == gridData.atomEnum && Mathf.Abs(k.level - gridData.atomLevel) < 0.1f);
            if (index < 0)
            {
                resultIngredientDataList.Add(new IngredientData()
                {
                    atomEnum = gridData.atomEnum,
                    level = gridData.atomLevel,
                    count = 1
                });
            }
            else
                resultIngredientDataList[index].count++;
        }
        return resultIngredientDataList;
        //return GenerateIngredientList(gameData.levelSessionDataList);
    }

    public List<IngredientData> GenerateIngredientList(List<LevelSessionData> levelSessionDataList)
    {
        var resultIngredientDataList = new List<IngredientData>();
        for (var i = 0; i < levelSessionDataList.Count; i++)
        {
            var levelSessionData = levelSessionDataList[i];
            for (var j = 0; j < levelSessionData.gridDataList.Count; j++)
            {
                var gridData = levelSessionData.gridDataList[j];
                if ((int)gridData.atomEnum >= 1000 || gridData.atomLevel < 4f)
                    continue;
                var index = resultIngredientDataList.FindIndex(k => k.atomEnum == gridData.atomEnum && Mathf.Abs(k.level - gridData.atomLevel) < 0.1f);
                if (index < 0)
                {
                    resultIngredientDataList.Add(new IngredientData()
                    {
                        atomEnum = gridData.atomEnum,
                        level = gridData.atomLevel,
                        count = 1
                    });
                }
                else
                    resultIngredientDataList[index].count++;
            }
        }
        return resultIngredientDataList;
    }

    public float GetLevelFromPrestigePoint(float prestigePoint)
    {
        return Mathf.Log(prestigePoint + 1f) + 1f;
    }

    public float GetIngredientCount(AtomEnum atomEnum, float level)
    {
        return gameData.levelSessionDataList.Sum(ls => ls.gridDataList.Count(g => g.atomEnum == atomEnum && Mathf.Abs(g.atomLevel - level) < 0.1f));
        //var myIngredient = gameData.ingredientList.Where(i => i.atomEnum == atomEnum && Mathf.Abs(i.level - level) < 0.1f).FirstOrDefault();
        //return myIngredient?.count ?? 0f;
    }

#region Save & Load
    [Obsolete("Please access LevelSessionData through gameData")]
    public void SaveSession(LevelSessionData levelSessionData)
    {
        throw new Exception("Method obsoleted");
        //var savePath = Path.Combine(DataManager.instance.GetBasePath(), $"{typeof(LevelSessionData)}_{levelSessionData.level}");
        //DataManager.instance.Save(levelSessionData, savePath);
    }

    [Obsolete("Please access LevelSessionData through gameData")]
    public LevelSessionData LoadSession(int level)
    {
        throw new Exception("Method obsoleted");
        //try
        //{
        //    var loadPath = Path.Combine(DataManager.instance.GetBasePath(), $"{typeof(LevelSessionData)}_{level}");
        //    return DataManager.instance.Load<LevelSessionData>(loadPath);
        //}
        //catch (Exception e)
        //{
        //    print(e);
        //    return default;
        //}
    }

    [Obsolete("Please access LevelSessionData through gameData")]
    public void RefreshSessionDataList()
    {
        throw new Exception("Method obsoleted");
        //try
        //{
        //    var result = new List<LevelSessionData>();
        //    var loadPath = DataManager.instance.GetBasePath();
        //    var files = Directory.GetFiles(loadPath);
        //    files = files.Where(i => i.Contains($"{typeof(LevelSessionData)}")).ToArray();
        //    foreach (var file in files)
        //        result.Add(DataManager.instance.Load<LevelSessionData>(file));
        //    gameData.levelSessionDataList = result;
        //    TempUpdateIngredient();
        //}
        //catch (Exception e)
        //{
        //    print(e);
        //}
    }
    #endregion

    // TODO: Wait for confirm
    public void TempUpdateIngredient()
    {
        throw new Exception("Method obsoleted");
        //var levelSessionDataList = gameData.levelSessionDataList;
        //gameData.ingredientList = new List<IngredientData>();
        //for (var i = 0; i < levelSessionDataList.Count; i++)
        //{
        //    for (var j = 0; j < levelSessionDataList[i].gridDataList.Count; j++)
        //    {
        //        var gridData = levelSessionDataList[i].gridDataList[j];
        //        if (gridData.atomEnum < AtomEnum.water_spawner && gridData.atomLevel >= 4)
        //        {
        //            TempAddIngredient(gridData.atomEnum, gridData.atomLevel);
        //        }
        //    }
        //}
    }

    public void TempAddIngredient(AtomEnum atomEnum, int level)
    {
        throw new Exception("Method obsoleted");
        //var targetIndex = gameData.ingredientList.FindIndex(i => i.atomEnum == atomEnum && i.level == level);
        //if (targetIndex != -1)
        //    gameData.ingredientList[targetIndex].count++;
        //else
        //    gameData.ingredientList.Add(new IngredientData()
        //    {
        //        atomEnum = atomEnum,
        //        level = level,
        //        count = 1
        //    });
    }
}

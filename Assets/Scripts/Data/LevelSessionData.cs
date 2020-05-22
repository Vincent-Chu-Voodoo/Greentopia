using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridData
{
    public AtomEnum atomEnum;
    public float totalCoolDown;
    public int atomLevel;
    public int rowIndex;
    public int columnIndex;
    public bool isDusty;
    public bool isCrate;
    public List<FTUE2IngredientBaseSData> ingredientSDataList;
}

[Serializable]
public class LevelSessionData
{
    public int level;
    public List<GridData> gridDataList;

    public LevelSessionData()
    {
        level = default;
        gridDataList = new List<GridData>();
    }

    public LevelSessionData(LevelSData levelSData)
    {
        level = levelSData.level;
        gridDataList = new List<GridData>();
        for (var i = 0; i < levelSData.spawnerDetailList.Count; i++)
            gridDataList.Add(levelSData.spawnerDetailList[i]);
    }
}

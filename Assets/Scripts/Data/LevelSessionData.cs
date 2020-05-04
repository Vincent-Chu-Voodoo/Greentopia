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
}

[Serializable]
public struct LevelSessionData
{
    public int level;
    public List<GridData> gridDataList;

    public LevelSessionData(LevelSData levelSData)
    {
        level = levelSData.level;
        gridDataList = new List<GridData>();
        for (var i = 0; i < levelSData.spawnerDetailList.Count; i++)
            gridDataList.Add(levelSData.spawnerDetailList[i]);
    }
}

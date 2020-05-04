using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AtomTypeAndLevel
{
    public AtomEnum type;
    public int level;
    public int howManyNeeded;
}

[Serializable]
public struct SpawnerDetail
{
    public AtomEnum spawnerType;
    public int rowIndex;
    public int columnIndex;
}

[Serializable]
public struct IngredientCollector
{
    public AtomEnum collectionType;
    public float collectionLevel;
}

[CreateAssetMenu(fileName = "LevelSData", menuName = "SData/LevelSData")]
public class LevelSData : ScriptableObject
{
    public int level;
    public float prestigeLevel;
    public int row;
    public int column;
    public float boardSize = 1f;
    public List<GridData> spawnerDetailList;

    public void OnValidate()
    {
        for (var i = 0; i < spawnerDetailList.Count; i++)
        {
            if (spawnerDetailList[i].totalCoolDown == 0f)
            {
                switch (spawnerDetailList[i].atomEnum)
                {
                    
                    case AtomEnum.water_spawner:
                        spawnerDetailList[i].totalCoolDown = 1.3f;
                        break;
                    default:
                        spawnerDetailList[i].totalCoolDown = 1.5f;
                        break;
                }
            }
        }
    }
}

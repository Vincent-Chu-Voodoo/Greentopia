using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Please use LevelSData")]
[CreateAssetMenu(fileName = "VictoryConditionSData", menuName = "SData/VictoryConditionSData")]
public class VictoryConditionSData : ScriptableObject
{
    public List<AtomTypeAndLevel> victoryConditionList;
}

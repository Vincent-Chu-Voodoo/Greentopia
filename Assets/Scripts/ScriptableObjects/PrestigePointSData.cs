using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrestigePointSData", menuName = "SData/PrestigePointSData")]
public class PrestigePointSData : ScriptableObject
{
    public List<float> accumulatedPrestigePointList;
}

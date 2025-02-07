﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IngredientData
{
    public AtomEnum atomEnum;
    public float level;
    public float count;
    //public List<GridData> gridDataList;
}

[Serializable]
public class PlantData
{
    public string plantName;
    public int count;
}

[Serializable]
public class TutorialData
{
    public bool iKnowTheStory;
    public bool iKnowWhatIsPrestige;
    public bool iKnowWhatIsGreenTile;
    public int iKnowIShouldNotMergeGreenTile = 3;
    public bool iKnowHowToMerge;
    public bool iKnowMultiBoard;
}

[Serializable]
public class GardenPlantData
{
    public int id;
    public PlantData plantData;
    public Vector3 localPosition;
    public Vector3 localScale;
}

[Serializable]
public class GameData
{
    public float prestigePoint;
    public TutorialData tutorialData = new TutorialData();
    public List<LevelSessionData> levelSessionDataList = new List<LevelSessionData>();
    //public List<IngredientData> ingredientList = new List<IngredientData>();
    //public List<PlantData> inventoryPlantList = new List<PlantData>();
    public List<GardenPlantData> gardentPlantList = new List<GardenPlantData>();
    
}

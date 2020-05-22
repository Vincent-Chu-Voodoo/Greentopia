using System;
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
    public int plantStage;
    public PlantStageEnum plantStageEnum;
    public Vector3 localPosition;
    public Vector3 localScale;
}

[Serializable]
public class GameData
{
    public List<FTUE2SellableSData> sellableList = new List<FTUE2SellableSData>();
    public List<FTUE2CrateSData> crateList = new List<FTUE2CrateSData>();
    public PlantSData pinnedPlant;

    public float prestigePoint;
    public float diamond = 100f;
    public float coin;
    public TutorialData tutorialData = new TutorialData();
    public LevelSessionData nurserySessionData = new LevelSessionData();
    public LevelSessionData toolShedSessionData = new LevelSessionData();
    public List<LevelSessionData> levelSessionDataList = new List<LevelSessionData>();
    //public List<IngredientData> ingredientList = new List<IngredientData>();
    //public List<PlantData> inventoryPlantList = new List<PlantData>();
    public List<GardenPlantData> gardentPlantList = new List<GardenPlantData>();
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FTUE2NurseryScene : MonoBehaviour
{
    public AtomSpawner atomSpawner;
    public List<SubGrid> subGridList;

    void Start()
    {
        var crateList = GameDataManager.instance.gameData.crateList.Where(i => i.targetBoard == FTUE2BoardEnum.NurseryBoard).ToList();
        
        print($"akaCK1: {crateList == null}");
        print($"akaCK2: {crateList.Count}");
        if (crateList != null && crateList.Count > 0)
        {
            var targetCrate = crateList.First();
            GameDataManager.instance.gameData.crateList.Remove(targetCrate);
            PopCrate(targetCrate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopCrate(FTUE2CrateSData fTUE2CrateSData)
    {
        foreach (var subGrid in subGridList)
            if (subGrid.isEmpty)
            {
                print($"PopCrate at: {subGrid.id}");
                atomSpawner.SpawnCrate(fTUE2CrateSData, subGrid);
                break;
            }
    }
}

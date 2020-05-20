using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FTUE2NurseryScene : MonoBehaviour
{
    public AtomSpawner atomSpawner;
    public List<SubGrid> subGridList;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        var crateList = GameDataManager.instance.gameData.crateList.Where(i => i.targetBoard == FTUE2BoardEnum.NurseryBoard).ToList();
        
        if (crateList != null && crateList.Count > 0)
        {
            var targetCrate = crateList.First();
            GameDataManager.instance.gameData.crateList.Remove(targetCrate);
            while (!subGridList.Exists(i => i.isEmpty))
                yield return null;
            atomSpawner.SpawnCrate(targetCrate, subGridList.Find(i => i.isEmpty));
        }
    }
}

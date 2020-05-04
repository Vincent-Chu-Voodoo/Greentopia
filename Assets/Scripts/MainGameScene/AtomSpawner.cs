using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AtomSpawner : MonoBehaviour
{
    [Header("Config")]
    public AssetReference atomAR;
    public GameObject atomPrefab;
    public MainGrid mainGrid;

    [Header("Event")]
    public GameEvent OnSpawnNewAtom;

    public void SpawnAtom(object spawnerObj)
    {
        var spawner = spawnerObj as Spawner;
        if (SpawnAtom(spawner))
            spawner.SpawnAtomSuccess();
        else
            spawner.SpawnAtomFailed();
    }

    public bool SpawnAtom(Spawner spawner)
    {
        for (var i = 0; i < spawner.spawnableSubGridList.Count; i++)
        {
            var subGrid = spawner.spawnableSubGridList[i];
            if (subGrid.isEmpty)
            {
                var newAtomGO = Instantiate(atomPrefab, subGrid.transform);
                newAtomGO.transform.position = spawner.transform.position;
                var newAtom = newAtomGO.GetComponent<Atom>();
                newAtom.Setup(spawner.atomType, mainGrid);
                newAtom.SubGridLinked(subGrid);
                subGrid.AtomLinked(newAtom);
                newAtom.AtomComplete();
                OnSpawnNewAtom.Invoke(newAtom);
                return true;
            }
        }
        return false;
    }

    public void SpawnAtom(GridData gridData)
    {
        var subGrid = mainGrid.GetGridAt(gridData.rowIndex, gridData.columnIndex);
        var aoh = atomAR.InstantiateAsync(subGrid.transform);
        aoh.Completed += _ =>
        {
            var newAtom = _.Result.GetComponent<Atom>();
            newAtom.Setup(gridData.atomEnum, mainGrid, gridData.totalCoolDown, gridData.atomLevel);
            newAtom.SubGridLinked(subGrid);
            subGrid.AtomLinked(newAtom);
            newAtom.AtomComplete();
            OnSpawnNewAtom.Invoke(newAtom);
        };
    }
}

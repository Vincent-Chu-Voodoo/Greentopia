using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FTUEShedSpawner : NewSpawner
{
    public int remainCount;
    public int maxCount;
    public int spawnCount;

    public GameEvent OnExhausted;

    private void Start()
    {
        remainCount = maxCount;
    }

    public override void SpawnAtom()
    {
        var emptyGridList = spawnableSubGridList.Where(i => i.isEmpty).ToList();
        for (var i = 0; i < spawnCount; i++)
        {
            if (emptyGridList.Count() < 1)
                break;
            var ranIndex = Random.Range(0, emptyGridList.Count());
            var targetGrid = emptyGridList[ranIndex];

            var newAtomGO = Instantiate(atomPrefab, targetGrid.transform);
            newAtomGO.transform.position = transform.position;
            var newAtom = newAtomGO.GetComponent<Atom>();
            newAtom.Setup(atomType, mainGrid);
            newAtom.SubGridLinked(targetGrid);
            targetGrid.AtomLinked(newAtom);
            newAtom.AtomComplete();
            OnSpawnNewAtom.Invoke(newAtom);
            atomController.SpawnNewAtom(newAtom);
            emptyGridList.RemoveAt(ranIndex);
            if (--remainCount < 1)
            {
                currentCoolDown = coolDown;
                remainCount = maxCount;
                OnExhausted.Invoke(this);
            }
        }
    }
}

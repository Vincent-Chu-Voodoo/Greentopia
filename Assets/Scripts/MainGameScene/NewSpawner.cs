using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
    public AtomEnum atomType;
    public GameObject atomPrefab;
    public MainGrid mainGrid;
    public List<SubGrid> spawnableSubGridList;
    public FTUEHandAnimation mergeCottonAnimation;

    public GameEvent OnClick;
    public GameEvent OnSpawnNewAtom;

    public void OnMouseUpAsButton()
    {
        if (!enabled)
            return;
        print($"OnMouseUpAsButton");
        OnClick.Invoke(this);
    }

    public virtual void SpawnAtom()
    {
        for (var i = 0; i < spawnableSubGridList.Count; i++)
        {
            var subGrid = spawnableSubGridList[i];
            if (subGrid != null && subGrid.isEmpty)
            {
                var newAtomGO = Instantiate(atomPrefab, subGrid.transform);
                newAtomGO.transform.position = transform.position;
                var newAtom = newAtomGO.GetComponent<Atom>();
                if (mergeCottonAnimation.anchorFrom == null)
                    mergeCottonAnimation.anchorFrom = newAtom.transform;
                else
                    mergeCottonAnimation.anchorTo = newAtom.transform;
                newAtom.Setup(atomType, mainGrid);
                newAtom.SubGridLinked(subGrid);
                subGrid.AtomLinked(newAtom);
                newAtom.AtomComplete();
                OnSpawnNewAtom.Invoke(newAtom);
            }
        }
    }
}

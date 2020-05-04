using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Display")]
    public float accumulationTime;
    public float coolDownRatio;
    public List<SubGrid> spawnableSubGridList;
    public MainGrid mainGrid;

    [Header("Param")]
    public AtomEnum atomType;
    public float totalCoolDownTime;

    [Header("Config")]
    public AtomDisplay atomDisplay;
    public Atom atom;

    [Header("Event")]
    public GameEvent OnSpawnAtom;
    public GameEvent OnSpawnAtomSuccess;

    void Update()
    {
        accumulationTime += Time.deltaTime;
        coolDownRatio = accumulationTime / totalCoolDownTime;
        if (coolDownRatio > 1f)
            Timesup();
        atomDisplay.SetPercent(coolDownRatio);
    }

    public void Setup(MainGrid _mainGrid, float coolDown)
    {
        if ((int)atom.atomType < 1000)
        {
            Destroy(this);
            return;
        }

        mainGrid = _mainGrid;
        totalCoolDownTime = coolDown;
        atomType = atom.atomType - 1000;
        OnSpawnAtom.AddListener(mainGrid.GetComponent<AtomSpawner>().SpawnAtom);
    }

    public void SubGridLinked(object _subGridObj)
    {
        SubGridLinked(_subGridObj as SubGrid);
    }

    public void SubGridUnlink(object _subGridObj)
    {
        SubGridUnlink(_subGridObj as SubGrid);
    }

    public void SubGridLinked(SubGrid _subGrid)
    {
        if (mainGrid != null)
            spawnableSubGridList = mainGrid.FindNeightbourSubgrids(_subGrid);
    }

    public void SubGridUnlink(SubGrid _subGrid)
    {
        spawnableSubGridList.Clear();
    }

    public void SpawnAtomSuccess()
    {
        OnSpawnAtomSuccess.Invoke(this);
        atomDisplay.SetShowPercent(true);
    }

    public void SpawnAtomFailed()
    {
        atomDisplay.SetShowPercent(false);
    }

    [ContextMenu("Timesup")]
    public void Timesup()
    {
        OnSpawnAtom.Invoke(this);
        accumulationTime = 0f;
        coolDownRatio = accumulationTime / totalCoolDownTime;
        atomDisplay.SetPercent(coolDownRatio);
    }
}

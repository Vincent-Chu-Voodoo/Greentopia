using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreCombineParam
{
    public bool canNotCombine;
    public Atom fromAtom;
    public Atom toAtom;
}

public class AtomController : MonoBehaviour
{
    [Header("Display")]
    public List<Atom> allAtomList;

    [Header("Param")]
    public Vector3 atomDragOffset;

    [Header("Config")]
    public MainGrid mainGrid;

    [Header("Event")]
    public GameEvent OnAtomSpawned;
    public GameEvent OnPreAtomCombined;
    public GameEvent OnAtomCombined;
    public GameEvent OnAtomDestroy;

    public int CountAtom(AtomEnum atomEnum, int atomLevel)
    {
        return allAtomList.Where(i => i.atomType == atomEnum && i.atomLevel == atomLevel).Count();
    }

    public void SpawnNewAtom(object atomObj)
    {
        SpawnNewAtom(atomObj as Atom);
    }

    public void PointerDown(object atomObj)
    {
        PointerDown(atomObj as Atom);
    }

    public void PointerDrag(object pointMoveParamObj)
    {
        PointerDrag(pointMoveParamObj as PointMoveParam);
    }

    public void PointerUp(object atomObj)
    {
        PointerUp(atomObj as Atom);
    }

    public void SpawnNewAtom(Atom atom)
    {
        allAtomList.Add(atom);
        OnAtomSpawned.Invoke(atom);
    }

    public void PointerDown(Atom atom)
    {
        if (atom.isDusty)
            return;
        atom.isAnchorToSubGrid = false;
    }

    public void PointerDrag(PointMoveParam pointMoveParam)
    {
        if (pointMoveParam.atom.isDusty)
            return;
        pointMoveParam.atom.transform.position = pointMoveParam.newWorldPoint + atomDragOffset;
    }

    public void PointerUp(Atom atom)
    {
        atom.isAnchorToSubGrid = true;
        if (atom.isDusty || (GetAtomCurrentGrid(atom)?.currentAtom?.isDusty ?? false))
        {
            if (!atom.CanCombine(GetAtomCurrentGrid(atom)?.currentAtom))
                return;
        }
        DropAtomToSubGrid(atom, GetAtomCurrentGrid(atom));
        if (GetAtomCurrentGrid(atom) == atom.subGrid)
            atom.Tap();
    }

    public SubGrid GetAtomCurrentGrid(Atom atom)
    {
        var ray = new Ray(atom.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.Grid.ToString())))
            return hitInfo.collider.GetComponent<SubGrid>();
        return null;
    }

    public void DropAtomToSubGrid(Atom atom, SubGrid targetSubGrid)
    {
        if (!ReferenceEquals(targetSubGrid, null))
        {
            if (targetSubGrid.currentAtom != null && targetSubGrid.currentAtom.CanCombine(atom))
                CombineAtom(atom, targetSubGrid.currentAtom);
            else
                TrySwapToGrid(atom, targetSubGrid);
        }

        atom.isAnchorToSubGrid = true;
    }

    public void TrySwapToGrid(Atom fromAtom, SubGrid targetSubGrid)
    {
        var droppingToAtom = targetSubGrid.currentAtom;
        var originalSubGrid = fromAtom.subGrid;

        droppingToAtom?.SubGridUnlink(targetSubGrid);
        targetSubGrid.AtomUnlink(droppingToAtom);
        originalSubGrid.AtomUnlink(fromAtom);
        fromAtom.SubGridUnlink(originalSubGrid);

        targetSubGrid.AtomLinked(fromAtom);
        fromAtom.SubGridLinked(targetSubGrid);
        originalSubGrid.AtomLinked(droppingToAtom);
        droppingToAtom?.SubGridLinked(originalSubGrid);
    }

    public void CombineAtom(Atom fromAtom, Atom toAtom)
    {
        var preCombineParam = new PreCombineParam()
        {
            fromAtom = fromAtom,
            toAtom = toAtom
        };
        OnPreAtomCombined.Invoke(preCombineParam);
        if (preCombineParam.canNotCombine)
            return;
        OnAtomDestroy.Invoke(fromAtom);
        RemoveAtom(fromAtom);
        Destroy(fromAtom.gameObject);
        toAtom.isDusty = false;
        toAtom.CombineAtom();
        OnAtomCombined.Invoke(toAtom);
    }

    public void RemoveAtom(Atom atom)
    {
        atom.subGrid.AtomUnlink(atom);
        allAtomList.RemoveAll(i => ReferenceEquals(atom, i));
    }

    public LevelSessionData GetLevelSessionData(int level)
    {
        var levelSessionData = new LevelSessionData
        {
            level = level,
            gridDataList = new List<GridData>()
        };
        foreach (var atom in allAtomList)
        {
            var gridData = new GridData();
            gridData.atomEnum = atom.atomType;
            gridData.rowIndex = (int) atom.subGrid.id.x;
            gridData.columnIndex = (int) atom.subGrid.id.y;
            gridData.atomLevel = atom.atomLevel;
            gridData.isDusty = atom.isDusty;
            gridData.isCrate = atom.isCrate;
            gridData.totalCoolDown = atom.GetComponent<Spawner>()?.totalCoolDownTime ?? 0f;
            levelSessionData.gridDataList.Add(gridData);
        }
        return levelSessionData;
    }
}

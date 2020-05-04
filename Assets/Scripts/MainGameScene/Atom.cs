using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Atom : MonoBehaviour, IAtom
{
    [Header("Display")]
    public AtomEnum atomType;
    public int atomLevel;
    public bool isAnchorToSubGrid;
    public bool isCanCraft;
    public SubGrid subGrid;

    [Header("Param")]
    public float anchoringSpeed;

    [Header("Config")]
    public AtomDisplay atomDisplay;
    public GameObject canCraftDisplay;
    public GameObject mergeHelpDisplay;

    [Header("Event")]
    public GameEvent OnAtomComplete;
    public GameEvent OnCombineAtom;
    public GameEvent OnSubgridLinked;
    public GameEvent OnSubgridUnlink;

    #region Monobehaviour
    void Start()
    {
        name = $"Atom {atomType.ToString()}";
    }

    void Update()
    {
        if (isAnchorToSubGrid)
            UpdateAnchoring();
    }
    #endregion

    #region IAtom
    [ContextMenu("CombineAtom")]
    public void CombineAtom()
    {
        atomLevel++;
        OnCombineAtom.Invoke(this);
    }
    #endregion

    public void Setup(AtomEnum _atomType, MainGrid mainGrid, float coolDown = 0f, int level = 1)
    {
        atomLevel = level;
        atomType = _atomType;
        GetComponent<Spawner>()?.Setup(mainGrid, coolDown);
    }

    public void AtomComplete()
    {
        OnAtomComplete.Invoke(this);
    }

    public void SubGridLinked(SubGrid _subGrid)
    {
        subGrid = _subGrid;
        transform.parent = _subGrid.transform;
        OnSubgridLinked.Invoke(_subGrid);
    }

    public void SubGridUnlink(SubGrid _subGrid)
    {
        subGrid = null;
        OnSubgridUnlink.Invoke(_subGrid);
    }

    public bool CanCombine(Atom atom)
    {
        return !ReferenceEquals(atom, this) && atom.atomLevel < 6 && atom == this;
    }

    private void UpdateAnchoring()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, anchoringSpeed * Time.deltaTime);
    }

    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
            return false;
        var otherAtom = other as Atom;
        return atomType == otherAtom.atomType && atomLevel == otherAtom.atomLevel;
    }

    public override int GetHashCode()
    {
        return $"{atomType}{atomLevel}".GetHashCode();
    }

    public static bool operator ==(Atom atom1, Atom atom2)
    {
        if (ReferenceEquals(atom1, atom2))
            return true;
        if (ReferenceEquals(atom1, null))
            return false;
        if (ReferenceEquals(atom2, null))
            return false;
        return atom1.Equals(atom2);
    }

    public static bool operator !=(Atom atom1, Atom atom2)
    {
        return !(atom1 == atom2);
    }

    public void SetCanCraft(bool _canCraft)
    {
        isCanCraft = _canCraft;
        canCraftDisplay.SetActive(_canCraft);
    }
}

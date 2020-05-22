using GameAnalyticsSDK.Setup;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Atom : MonoBehaviour, IAtom
{
    [Header("Display")]
    public AtomEnum atomType;
    public int atomLevel;
    public bool isDusty;
    public bool isAnchorToSubGrid;
    public bool isCanCraft;
    public bool isCrate;
    public SubGrid subGrid;
    public AtomSpawner atomSpawner;
    public List<FTUE2IngredientBaseSData> ingredientSDataList;

    [Header("Param")]
    public float anchoringSpeed;

    [Header("Config")]
    public AtomDisplay atomDisplay;
    public GameObject canCraftDisplay;
    public GameObject canCraftDisplay2;
    public GameObject mergeHelpDisplay;

    [Header("Event")]
    public GameEvent OnAtomComplete;
    public GameEvent OnCombineAtom;
    public GameEvent OnSubgridLinked;
    public GameEvent OnSubgridUnlink;

    #region Monobehaviour
    void Start()
    {
        name = $"Atom {atomType}";
        //atomDisplay.UpdateDisplay(this);
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

    public void Setup(AtomEnum _atomType, MainGrid mainGrid, float coolDown = 0f, int level = 1, bool _isDusty = false)
    {
        isDusty = _isDusty;
        atomLevel = level;
        atomType = _atomType;
        GetComponent<Spawner>()?.Setup(mainGrid, coolDown);
    }

    public void SetupAsCrate(AtomEnum _atomType, List<FTUE2IngredientBaseSData> data, AtomSpawner _atomSpawner)
    {
        atomSpawner = _atomSpawner;
        isCrate = true;
        atomType = _atomType;
        ingredientSDataList = data;
        Destroy(GetComponent<Spawner>());
    }

    public void SetupAsCrate(FTUE2CrateSData fTUE2CrateSData, AtomSpawner _atomSpawner)
    {
        atomSpawner = _atomSpawner;
        isCrate = true;
        atomType = fTUE2CrateSData.atomEnum;
        ingredientSDataList = new List<FTUE2IngredientBaseSData>();
        var tempList = new List<FTUE2IngredientBaseSData>();
        for (var i = 0; i < fTUE2CrateSData.fTUE2IngredientBaseSDataList.Count; i++) {
            tempList.Add(fTUE2CrateSData.fTUE2IngredientBaseSDataList[i]);
        }
        if (fTUE2CrateSData.randomize)
        {
            while (tempList.Count > 0)
            {
                var index = Random.Range(0, tempList.Count);
                ingredientSDataList.Add(tempList[index]);
                tempList.RemoveAt(index);
            }
        }
        else
            ingredientSDataList = tempList;
        
        Destroy(GetComponent<Spawner>());
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
        return !(atom is null) &&
            !(atom.atomType == AtomEnum.cotton && atom.atomLevel == 4) &&
            !ReferenceEquals(atom, this) && (!atom.isDusty || !isDusty) && atom.atomLevel < 6 && atom == this && !isCrate;
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
        print($"SetCanCraft {_canCraft}");
        isCanCraft = _canCraft;
        canCraftDisplay.SetActive(_canCraft);
        canCraftDisplay2?.SetActive(_canCraft);
    }

    public void SetFTUEGreen()
    {
        atomDisplay.SetFTUEGreen();
    }

    public void Tap()
    {
        if (isCrate)
        {
            if (ingredientSDataList.Count > 0)
            {
                var ingredient = ingredientSDataList.First();
                ingredient.Reset();
                if (atomSpawner.SpawnAtom(ingredient.atomEnum, ingredient.atomLevel, ingredient.isDusty))
                    ingredientSDataList.RemoveAt(0);
                if (ingredientSDataList.Count == 0)
                {
                    FindObjectOfType<AtomController>().RemoveAtom(this);
                    Destroy(gameObject);
                }
            }
            else
            {
                FindObjectOfType<AtomController>().RemoveAtom(this);
                Destroy(gameObject);
            }
        }
    }
}

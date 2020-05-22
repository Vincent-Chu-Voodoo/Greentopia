using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSpawner : MonoBehaviour
{
    public GameObject coolDownDisplayGO;
    public Image fillImage;

    public float currentCoolDown
    {
        get
        {
            if (atomType == AtomEnum.cotton)
                return cottonCurrentCoolDown;
            else if (atomType == AtomEnum.water)
                return waterCurrentCoolDown;
            else
                throw new System.Exception();
        }
        set
        {
            if (atomType == AtomEnum.cotton)
                cottonCurrentCoolDown = value;
            else if (atomType == AtomEnum.water)
                waterCurrentCoolDown = value;
            else
                throw new System.Exception();
        }
    }
    public float coolDown;

    public static float cottonCurrentCoolDown = 0f;
    public static float waterCurrentCoolDown = 0f;

    public AtomEnum atomType;
    public GameObject atomPrefab;
    public MainGrid mainGrid;
    public List<SubGrid> spawnableSubGridList;
    public AtomController atomController;

    public GameEvent OnClick;
    public GameEvent OnSpawnNewAtom;

    [ContextMenu("Print")]
    public void Print()
    {
        print($"currentCoolDown: {currentCoolDown}");
    }

    public float previousTime
    {
        get
        {
            if (atomType == AtomEnum.cotton)
                return cottonPreviousTime;
            else if (atomType == AtomEnum.water)
                return waterPreviousTime;
            else
                throw new System.Exception();
        }
        set
        {
            if (atomType == AtomEnum.cotton)
                cottonPreviousTime = value;
            else if (atomType == AtomEnum.water)
                waterPreviousTime = value;
            else
                throw new System.Exception();
        }
    }
    public static float cottonPreviousTime;
    public static float waterPreviousTime;

    private void Update()
    {
        var timeDelta = Time.realtimeSinceStartup - previousTime;
        previousTime = Time.realtimeSinceStartup;
        currentCoolDown = Mathf.MoveTowards(currentCoolDown, 0f, timeDelta);
        coolDownDisplayGO.SetActive(currentCoolDown > 0f);
        fillImage.fillAmount = (coolDown - currentCoolDown) / coolDown;
    }

    public void OnMouseUpAsButton()
    {
        if (!enabled)
            return;
        if (currentCoolDown > 0f)
            return;
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
                newAtom.Setup(atomType, mainGrid);
                newAtom.SubGridLinked(subGrid);
                subGrid.AtomLinked(newAtom);
                newAtom.AtomComplete();
                atomController.SpawnNewAtom(newAtom);
                OnSpawnNewAtom.Invoke(newAtom);
            }
        }
    }
}

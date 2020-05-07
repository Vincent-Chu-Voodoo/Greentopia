using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtomDisplay : MonoBehaviour
{
    [Header("Header")]
    public bool isShowPercent;

    [Header("Config")]
    public Transform percentTrans;
    public SpriteRenderer spriteRendererFG;
    public SpriteRenderer spriteRendererBG;
    public List<AtomDisplaySData> atomDisplayDataList;
    public GameObject fTUEGreen;

    [ContextMenu("SortAtomDisplayDataList")]
    public void SortAtomDisplayDataList()
    {
        atomDisplayDataList.Sort((i, j) => j.type.CompareTo(i.type));
    }

    public void SetShowPercent(bool value)
    {
        isShowPercent = value;
        if (!isShowPercent)
            percentTrans.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetPercent(float value)
    {
        if (isShowPercent)
            percentTrans.transform.localScale = new Vector3(1f, value, 1f);
    }

    public void CombineAtom(object atomObj)
    {
        CombineAtom(atomObj as Atom);
    }

    public void CombineAtom(Atom atom)
    {
        UpdateDisplay(atom);
    }

    public void AtomComplete(object atomObj)
    {
        AtomComplete(atomObj as Atom);
    }

    public void AtomComplete(Atom atom)
    {
        UpdateDisplay(atom);
    }

    public void UpdateDisplay(Atom atom)
    {
        var aoh = ResourceManager.instance.GetAtomSpriteAOH(atom.atomType, atom.atomLevel);
        aoh.Completed += _ =>
        {
            if (spriteRendererBG != null)
                spriteRendererBG.sprite = aoh.Result as Sprite;
            if (spriteRendererFG != null)
                spriteRendererFG.sprite = aoh.Result as Sprite;
        };
    }

    public void SetFTUEGreen()
    {
        fTUEGreen.SetActive(true);
    }
}

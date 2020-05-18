using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGrid : MonoBehaviour
{
    [Header("Display")]
    public Vector2 id;
    public Vector2 gridSize;
    public Atom currentAtom;

    [Header("Param")]
    public float anchorAtomSpeed;

    [Header("Event")]
    public GameEvent OnAtomLinked;
    public GameEvent OnAtomUnLinked;

    public bool isEmpty { get { return gameObject.activeSelf && currentAtom == null; } }

    void Update()
    {
        if (currentAtom)
            currentAtom.transform.localPosition = Vector3.MoveTowards(currentAtom.transform.localPosition, Vector3.zero, anchorAtomSpeed * Time.deltaTime);
    }

    public void AtomLinked(object atomObj)
    {
        AtomLinked(atomObj as Atom);
    }

    public void AtomLinked(Atom atom)
    {
        currentAtom = atom;
        OnAtomLinked.Invoke(atom);
    }

    public void AtomUnLinked(object atomObj)
    {
        AtomUnlink(atomObj as Atom);
    }

    public void AtomUnlink(Atom atom)
    {
        currentAtom = null;
        OnAtomUnLinked.Invoke(atom);
    }
}

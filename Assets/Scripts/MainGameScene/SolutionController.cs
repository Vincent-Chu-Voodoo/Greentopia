using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MergeSolution
{
    public Atom atom1;
    public Atom atom2;
}

public class SolutionController : MonoBehaviour
{
    public AtomController atomController;

    [ContextMenu("FindSolutionTest")]
    public void FindSolutionTest()
    {
        FindSolution(atomController.allAtomList);
    }

    public List<MergeSolution> FindSolution()
    {
        return FindSolution(atomController.allAtomList);
    }

    public List<MergeSolution> FindSolution(List<Atom> atomList)
    {
        var solutionList = new List<MergeSolution>();
        var tempAtomList = new List<Atom>(atomList);

        while (tempAtomList.Count > 0)
        {
            var target = tempAtomList[0];
            tempAtomList.RemoveAt(0);
            if (target.isCanCraft)
                continue;
            var answerAtom = tempAtomList.Where(i => i.CanCombine(target)).FirstOrDefault();
            if (answerAtom != null)
                solutionList.Add(new MergeSolution() { atom1 = target, atom2 = answerAtom });
        }

        return solutionList;
    }
}

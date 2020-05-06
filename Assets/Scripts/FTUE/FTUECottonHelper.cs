using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FTUECottonHelper : MonoBehaviour
{
    public AtomEnum targetType;
    public FTUEHandAnimation fTUEHandAnimation;
    public AtomController atomController;
    public SolutionController solutionController;

    public void UpdateFinger()
    {
        var atomList = atomController.allAtomList.Where(i => i.atomType == targetType).ToList();
        var solution = solutionController.FindSolution(atomList);
        print($"{name} UpdateFinger {solution.Count}");
        if (solution.Count > 0)
        {
            fTUEHandAnimation.anchorFrom = solution.First().atom1.transform;
            fTUEHandAnimation.anchorTo = solution.First().atom2.transform;
        }
    }

    public void OnMerge()
    {
        UpdateFinger();
    }

    public void OnSpawnAtom()
    {
        UpdateFinger();
    }
}

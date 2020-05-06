using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeHighlightHelper : MonoBehaviour
{
    [Header("Display")]
    public MergeSolution solution;

    [Header("Param")]
    public float waitTime;

    [Header("Config")]
    public SolutionController solutionController;

    public Coroutine helperRoutine;

    void Start()
    {
        ResetHelper();
    }

    public void OnPointerDown()
    {
        if (enabled)
            ResetHelper();
    }

    private void ResetHelper()
    {
        if (!gameObject.activeSelf)
            return;
        ResetHelp();
        if (helperRoutine != null)
            StopCoroutine(helperRoutine);
        helperRoutine = StartCoroutine(HelperLoop());
    }

    public IEnumerator HelperLoop()
    {
        yield return new WaitForSeconds(waitTime);
        Help();
    }

    public void ResetHelp()
    {
        var mergeHelpDisplay = solution?.atom1?.mergeHelpDisplay;
        if (mergeHelpDisplay != null)
            mergeHelpDisplay.SetActive(false);
        mergeHelpDisplay = solution?.atom2?.mergeHelpDisplay;
        if (mergeHelpDisplay != null)
            mergeHelpDisplay.SetActive(false);
    }

    public void Help()
    {
        solution = solutionController.FindSolution().FirstOrDefault();
        if (ReferenceEquals(solution, null))
        {
            ResetHelper();
            return;
        }
        solution.atom1.mergeHelpDisplay.SetActive(true);
        solution.atom2.mergeHelpDisplay.SetActive(true);
    }
}

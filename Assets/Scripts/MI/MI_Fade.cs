using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MI_Fade : MonoBehaviour, IPassive
{
    [Header("Param")]
    public bool fadeIn;
    public float duration;

    [Header("Config")]
    public Image fadeImage;

    private Coroutine actionRoutine;

    public void Begin(object obj)
    {
        if (actionRoutine != null)
            StopCoroutine(actionRoutine);
        actionRoutine = StartCoroutine(BeginLoop());
    }

    IEnumerator BeginLoop()
    {
        var accumTime = 0f;
        var ratio = 0f;
        while (ratio < 1f)
        {
            ratio = Mathf.Lerp(0f, 1f, accumTime / duration);
            yield return null;
        }
    }
}

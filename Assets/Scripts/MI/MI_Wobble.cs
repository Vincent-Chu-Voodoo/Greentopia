using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Wobble : MonoBehaviour, IPassive
{
    [Header("Param")]
    public float duration;
    public float magnitude;
    public float repitition;

    [Header("Config")]
    public Transform targetTrans;

    private Vector3 originalLocalScale;
    private Coroutine actionRoutine;

    [ContextMenu("Begin")]
    public void Begin()
    {
        if (actionRoutine != null)
        {
            StopCoroutine(actionRoutine);
            targetTrans.localScale = originalLocalScale;
        }
        actionRoutine = StartCoroutine(BeginLoop());
    }

    public void Begin(object obj)
    {
        if (actionRoutine != null)
        {
            StopCoroutine(actionRoutine);
            targetTrans.localScale = originalLocalScale;
        }
        actionRoutine = StartCoroutine(BeginLoop());
    }

    IEnumerator BeginLoop()
    {
        originalLocalScale = targetTrans.localScale;
        var targetScale = originalLocalScale * magnitude;

        for (var i = 0; i < repitition; i++)
        {
            var ratio = 0f;
            var timeAccum = 0f;

            while (ratio < 1f)
            {
                if (ratio < 0.5f)
                    targetTrans.localScale = Vector3.Lerp(originalLocalScale, targetScale, 2 * ratio);
                else
                    targetTrans.localScale = Vector3.Lerp(originalLocalScale, targetScale, 2 - 2 * ratio);
                timeAccum += Time.deltaTime;
                ratio = timeAccum / duration;
                yield return null;
            }
        }
        targetTrans.localScale = originalLocalScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Growth : MonoBehaviour, IPassive
{
    [Header("Param")]
    public float growthSpeed;
    public Vector3 startScale;
    public Vector3 targetScale;

    [Header("Config")]
    public Transform targetTrans;

    public void Begin(object obj)
    {
        StartCoroutine(BeginLoop());
    }

    IEnumerator BeginLoop()
    {
        transform.localScale = startScale;
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, growthSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localScale = targetScale;
    }
}

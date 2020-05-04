using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTo : MonoBehaviour
{
    [Header("Display")]
    public Coroutine toRoutine;

    [Header("Param")]
    public float offset;
    public float maxScrollTime;
    public float scrollSpeed;

    [Header("Config")]
    public ScrollRect scrollRect;
    public RectTransform contentPanel;

    public void To(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();
        if (toRoutine != null)
            StopCoroutine(toRoutine);
        toRoutine = StartCoroutine(ToLoop(target));
    }

    IEnumerator ToLoop(RectTransform target)
    {
        var accumTime = 0f;
        var targetPosition = new Vector2(Mathf.Min(offset - target.anchoredPosition.x, 0f), 0f);
        while (accumTime < maxScrollTime && Vector2.Distance(targetPosition, target.anchoredPosition) > 0.1f)
        {
            contentPanel.anchoredPosition = Vector2.MoveTowards(contentPanel.anchoredPosition, targetPosition, Time.deltaTime * scrollSpeed);
            accumTime += Time.deltaTime;
            yield return null;
        }
        contentPanel.anchoredPosition = targetPosition;
    }
}

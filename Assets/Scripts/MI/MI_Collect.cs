using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Collect : MonoBehaviour, IPassive
{
    public float moveSpeed;
    public Vector3 collectionTargetPosition;
    public GameEvent OnEnd = new GameEvent();

    public void Begin(object obj)
    {
        StartCoroutine(BeginLoop());
    }

    IEnumerator BeginLoop()
    {
        while (Vector3.Distance(transform.position, collectionTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, collectionTargetPosition, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.one * Mathf.Clamp(Vector3.Distance(transform.position, collectionTargetPosition), 0f, 1f);
            yield return null;
        }
        OnEnd.Invoke(this);
    }
}

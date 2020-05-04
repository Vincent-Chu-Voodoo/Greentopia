using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquirrelModelAnimState
{
    idle, run
}

public class SquirrelModelController : MonoBehaviour
{
    public float waitTime;

    public float moveSpeed;
    public Vector2 waitTimeRange;
    public Vector3 centerAnchor;
    public float radius;

    public Animator animator;

    private Coroutine actionRoutine;

    IEnumerator Start()
    {
        while (true)
        {
            var nextPosition = centerAnchor + Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up) * radius;
            yield return MoveToLoop(nextPosition);
            waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (actionRoutine != null)
            StopCoroutine(actionRoutine);
        actionRoutine = StartCoroutine(MoveToLoop(targetPosition));
    }

    IEnumerator MoveToLoop(Vector3 targetPosition)
    {
        var characterTrans = animator.transform;
        characterTrans.LookAt(targetPosition);
        animator.Play(SquirrelModelAnimState.run.ToString());

        while (Vector3.Distance(targetPosition, characterTrans.position) > 0f)
        {
            characterTrans.position = Vector3.MoveTowards(characterTrans.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.Play(SquirrelModelAnimState.idle.ToString());
    }
}

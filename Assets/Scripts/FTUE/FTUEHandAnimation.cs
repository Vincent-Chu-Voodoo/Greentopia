using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEHandAnimation : MonoBehaviour
{
    public float speed;
    public float stayTime;
    public Transform anchorFrom;
    public Transform anchorTo;

    IEnumerator Start()
    {
        while (anchorFrom == null || anchorTo == null)
            yield return null;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, anchorTo.position, speed * Time.deltaTime);
            yield return null;
            if (Vector3.Distance(transform.position, anchorTo.position) < 0.1f)
            {
                yield return new WaitForSeconds(stayTime);
                transform.position = anchorFrom.position;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEHandAnimation : MonoBehaviour
{
    public float startDelay;
    public float speed;
    public float stayTime;

    public GameObject rootGO;
    public Transform anchorFrom;
    public Transform anchorTo;

    IEnumerator Start()
    {
        rootGO.SetActive(false);
        yield return new WaitForSeconds(startDelay);
        while (anchorFrom == null || anchorTo == null)
            yield return null;
        transform.position = anchorFrom.position;
        rootGO.SetActive(true);
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

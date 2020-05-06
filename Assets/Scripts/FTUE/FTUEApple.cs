using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEApple : MonoBehaviour
{
    public float collectionSpeed;
    public Transform collectionAnchor;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, collectionAnchor.position, collectionSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, collectionAnchor.position) < 0.1f)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEReanchor : MonoBehaviour
{
    public Camera targetCamera;
    public Transform plantTransform;

    void Update()
    {
        var sp = targetCamera.WorldToScreenPoint(plantTransform.position);
        transform.position = targetCamera.ScreenToWorldPoint(new Vector3(sp.x, sp.y, transform.position.z));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2RootTrace : MonoBehaviour
{
    public Transform traceTarget;
    
    void Update()
    {
        transform.position = new Vector3(traceTarget.position.x, traceTarget.position.y, traceTarget.position.z);
    }
}

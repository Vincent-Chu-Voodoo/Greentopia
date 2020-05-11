using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUECollectionAnchor : MonoBehaviour
{
    public float maxRadius;
    public List<Transform> movedWithAnchorList;
    public List<Transform> movedWithAnchorList2;

    public void OnMoved()
    {
        return;
        var wp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8f));
        var delta = wp - transform.position;
        delta = delta.normalized * maxRadius;
        transform.Translate(delta);
        foreach (var t in movedWithAnchorList)
            t.Translate(delta);
        foreach (var t in movedWithAnchorList2)
            t.Translate(2f * delta);
        print($"{wp} {delta}");
    }
}

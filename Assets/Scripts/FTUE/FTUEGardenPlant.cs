using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEGardenPlant : MonoBehaviour
{
    public bool isDragging;
    public bool isMoved;
    public float anchorSpeed;
    public Camera refCam;
    public Transform targetAnchor;
    public GameEvent OnMoved;

    private void Update()
    {
        if (isMoved)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetAnchor.position, anchorSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDrag()
    {
        if (isMoved)
            return;
        isDragging = true;
        var mp = Input.mousePosition;
        var wp = refCam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, 8f));
        transform.position = wp;
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isMoved = true;
            OnMoved.Invoke(this);
        }
    }
}

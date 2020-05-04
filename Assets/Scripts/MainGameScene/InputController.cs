using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMoveParam
{
    public Atom atom;
    public Vector3 newWorldPoint;
}

public class InputController : MonoBehaviour
{
    [Header("Config")]
    public Camera referenceCamera;

    [Header("Event")]
    public GameEvent OnPointerDownAtom;
    public GameEvent OnPointerDragAtom;
    public GameEvent OnPointerUpAtom;

    private Atom activeAtom;

    void Update()
    {
#if UNITY_EDITOR
        UpdateEditor();
#else
        UpdateMobile();
#endif
    }

    public void UpdateEditor()
    {
        if (Input.GetMouseButtonDown(0))
            PointerDown(Input.mousePosition);

        if (Input.GetMouseButton(0))
            PointerMove(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
            PointerUp();
    }

    public void UpdateMobile()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.touches[0];
        if (touch.phase == TouchPhase.Began)
            PointerDown(Input.mousePosition);

        PointerMove(Input.mousePosition);

        if (touch.phase == TouchPhase.Ended)
            PointerUp();
    }

    public void PointerDown(Vector3 screenPoint)
    {
        var ray = referenceCamera.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.Atom.ToString())))
        {
            activeAtom = hitInfo.collider.GetComponent<Atom>();
            OnPointerDownAtom.Invoke(activeAtom);
        }
    }

    public void PointerMove(Vector3 screenPoint)
    {
        if (activeAtom == null)
            return;
        var worldPoint = referenceCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, referenceCamera.transform.position.y));
        var param = new PointMoveParam() { atom = activeAtom, newWorldPoint = worldPoint };
        OnPointerDragAtom.Invoke(param);
    }

    public void PointerUp()
    {
        if (activeAtom != null)
        {
            OnPointerUpAtom.Invoke(activeAtom);
            activeAtom = null;
        }
    }
}

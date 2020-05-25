using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PlantOnCardBehaviour : MonoBehaviour
{
    public Camera targetCamera;
    public Transform defaultPlantAnchor;
    public GameEvent OnPlanted = new GameEvent();

    private void Start()
    {
        targetCamera = Camera.main;
        defaultPlantAnchor = GameObject.FindGameObjectWithTag(TagEnum.DefaultPlantAnchor.ToString()).transform;
        OnPlanted.AddListener(GetComponent<FTUE2Plant>().Planted);
    }

    private void OnMouseDrag()
    {
        var sp = Input.mousePosition;
        transform.position = targetCamera.ScreenToWorldPoint(new Vector3(sp.x, sp.y, transform.position.z));
    }

    private void OnMouseUp()
    {
        print($"Plant OnMouseUp");
        var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.PlantBase.ToString())))
        {
            print($"Hit {hitInfo.collider.name}");
        }
        else
        {
            transform.position = defaultPlantAnchor.position;
        }
        OnPlanted.Invoke(this);
    }
}

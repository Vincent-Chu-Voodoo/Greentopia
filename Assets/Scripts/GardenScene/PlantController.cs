using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    [Header("Display")]
    public GameObject activeObject;

    [Header("Param")]
    public float increaseStep;
    public Vector3 plantMinScale;
    public Vector3 plantMaxScale;

    [Header("Config")]
    public Camera referenceCamera;
    public GameObject controlPanelGO;
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        controlPanelGO.SetActive(false);
    }

    void Update()
    {
        var pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        if (results.Count > 0)
            return;
        var ray = referenceCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo = default;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.Plant.ToString())))
            {
                activeObject = hitInfo.collider.gameObject;
                activeObject.GetComponent<Plant>()?.PointerDown();
            }
            else
                activeObject = null;
            controlPanelGO.SetActive(activeObject);
        }
        if (Input.GetMouseButton(0))
        {
            if (activeObject)
            {
                if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.PlantBase.ToString())))
                    activeObject.transform.position = hitInfo.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (activeObject)
            {
                activeObject.GetComponent<Plant>()?.PointerUp();
                //GameDataManager.instance.gameData.gardentPlantList.Find(i => i.id == activeObject.GetComponent<Plant>().plantId).localPosition = activeObject.transform.localPosition;
            }
        }
    }

    public void ScaleUp()
    {
        if (activeObject)
        {
            activeObject.transform.localScale = 
                Vector3.MoveTowards(
                    activeObject.transform.localScale, 
                    new Vector3(activeObject.transform.localScale.x > 0 ? plantMaxScale.x : -plantMaxScale.x, plantMaxScale.y, plantMaxScale.z), 
                    increaseStep
                    );
            GameDataManager.instance.gameData.gardentPlantList.Find(i => i.id == activeObject.GetComponent<Plant>().plantId).localScale = activeObject.transform.localScale;
        }
    }

    public void ScaleDown()
    {
        if (activeObject)
        {
            activeObject.transform.localScale = 
                Vector3.MoveTowards(
                    activeObject.transform.localScale,
                    new Vector3(activeObject.transform.localScale.x > 0 ? plantMinScale.x : -plantMinScale.x, plantMinScale.y, plantMinScale.z),
                    increaseStep
                    );
            GameDataManager.instance.gameData.gardentPlantList.Find(i => i.id == activeObject.GetComponent<Plant>().plantId).localScale = activeObject.transform.localScale;
        }
    }

    public void Flip()
    {
        if (activeObject)
        {
            activeObject.transform.localScale = new Vector3(-activeObject.transform.localScale.x, activeObject.transform.localScale.y, activeObject.transform.localScale.z);
            GameDataManager.instance.gameData.gardentPlantList.Find(i => i.id == activeObject.GetComponent<Plant>().plantId).localScale = activeObject.transform.localScale;
        }
    }
}

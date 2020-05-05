using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FTUEGardenPlantStatus
{
    NotPlanted, NeedNutrition, HaveNutrition, Growing, Grown
}

public class FTUEGardenPlant : MonoBehaviour
{
    public static FTUEGardenPlantStatus fTUEGardenPlantStatus;
    public static bool haveNutrition;
    public bool isDragging;
    public bool isMoved;
    public float anchorSpeed;
    public Camera refCam;
    public Transform targetAnchor;

    public GameEvent OnMoved;
    public GameEvent OnFedNutrition;
    public GameEvent OnSpeededUp;
    public GameEvent OnGrown;
    public GameEvent OnCollectApple;

    private void Start()
    {
        if (haveNutrition && fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.HaveNutrition;
    }

    private void Update()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
            transform.position = Vector3.MoveTowards(transform.position, targetAnchor.position, anchorSpeed * Time.deltaTime);
        if (fTUEGardenPlantStatus != FTUEGardenPlantStatus.NotPlanted && fTUEGardenPlantStatus != FTUEGardenPlantStatus.NeedNutrition)
            transform.position = targetAnchor.position;

    }

    private void OnMouseDrag()
    {
        if (fTUEGardenPlantStatus != FTUEGardenPlantStatus.NotPlanted)
            return;
        var mp = Input.mousePosition;
        var wp = refCam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, 8f));
        transform.position = wp;
    }

    private void OnMouseUp()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.NotPlanted)
        {
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.NeedNutrition;
            OnMoved.Invoke(this);
        }
    }

    public void Grown()
    {
        OnGrown.Invoke(this);
    }

    public void SpeedUp()
    {
        OnSpeededUp.Invoke(this);
    }

    [ContextMenu("LogEnum")]
    public void LogEnum()
    {
        print(fTUEGardenPlantStatus);
    }
}

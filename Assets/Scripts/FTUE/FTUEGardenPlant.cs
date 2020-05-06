using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FTUEGardenPlantStatus
{
    NotPlanted, NeedNutrition, HaveNutrition, Growing, Grown, CanCollectApple
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
    public GameEvent OnHadNutrition;
    public GameEvent OnFedNutrition;
    public GameEvent OnClickWhileGrowing;
    public GameEvent OnSpeededUp;
    public GameEvent OnGrown;
    public GameEvent OnCollectApple;
    public GameEvent OnMoveApple;

    private void Start()
    {
        print($"haveNutrition: {haveNutrition} {fTUEGardenPlantStatus}");
        if (haveNutrition && fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
        {
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.HaveNutrition;
            OnHadNutrition.Invoke(this);
        }
    }

    private void Update()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
            transform.position = Vector3.MoveTowards(transform.position, targetAnchor.position, anchorSpeed * Time.deltaTime);
        if (fTUEGardenPlantStatus != FTUEGardenPlantStatus.NotPlanted && fTUEGardenPlantStatus != FTUEGardenPlantStatus.NeedNutrition)
            transform.position = targetAnchor.position;

    }

    public void FedNutrition()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.HaveNutrition)
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.Growing;
        OnFedNutrition.Invoke(this);
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
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.Growing)
        {
            OnClickWhileGrowing.Invoke(this);
        }
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.NotPlanted)
        {
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.NeedNutrition;
            OnMoved.Invoke(this);
        }
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.CanCollectApple)
        {
            CollectApple();
        }
    }

    public void Grown()
    {
        fTUEGardenPlantStatus = FTUEGardenPlantStatus.Grown;
        OnGrown.Invoke(this);
    }

    public void SpeedUp()
    {
        OnSpeededUp.Invoke(this);
    }

    public void CollectApple()
    {
        OnCollectApple.Invoke(this);
    }

    public void MoveAppleToBasket()
    {
        OnMoveApple.Invoke(this);
    }

    [ContextMenu("LogEnum")]
    public void LogEnum()
    {
        print(fTUEGardenPlantStatus);
    }
}

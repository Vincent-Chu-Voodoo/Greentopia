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
    public static Vector3 currentPosition;
    public bool useMI;
    public float anchorSpeed;
    public Camera refCam;
    public Transform targetAnchor;

    public Vector3 grownBoxPos;
    public Vector3 grownBoxScale;

    public GameEvent OnMoved;
    public GameEvent OnHadNutrition;
    public GameEvent OnFedNutrition;
    public GameEvent OnClickWhileGrowing;
    public GameEvent OnSpeededUp;
    public GameEvent OnGrown;
    public GameEvent OnCollectApple;
    public GameEvent OnMoveApple;
    public GameEvent OnTap;

    private void Start()
    {
        if (haveNutrition && fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
        {
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.HaveNutrition;
            OnHadNutrition.Invoke(this);
        }
        if (fTUEGardenPlantStatus != FTUEGardenPlantStatus.NotPlanted && fTUEGardenPlantStatus != FTUEGardenPlantStatus.NeedNutrition)
        {
            transform.position = currentPosition;
        }
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.CanCollectApple)
        {
            GetComponent<BoxCollider>().center = grownBoxPos;
            GetComponent<BoxCollider>().size = grownBoxScale;
        }
    }

    private void Update()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.NeedNutrition)
            transform.position = Vector3.MoveTowards(transform.position, targetAnchor.position, anchorSpeed * Time.deltaTime);
        currentPosition = transform.position;
    }

    public void FedNutrition()
    {
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.HaveNutrition)
            fTUEGardenPlantStatus = FTUEGardenPlantStatus.Growing;
        OnFedNutrition.Invoke(this);
    }

    public void DisableMI()
    {
        useMI = false;
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
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out _, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.PlantBase.ToString())))
                transform.position = targetAnchor.position;
            OnMoved.Invoke(this);
        }
        if (fTUEGardenPlantStatus == FTUEGardenPlantStatus.CanCollectApple)
        {
            CollectApple();
        }
        if (useMI)
            OnTap.Invoke(this);
    }

    public void Grown()
    {
        fTUEGardenPlantStatus = FTUEGardenPlantStatus.Grown;
        GetComponent<BoxCollider>().center = grownBoxPos;
        GetComponent<BoxCollider>().size = grownBoxScale;
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

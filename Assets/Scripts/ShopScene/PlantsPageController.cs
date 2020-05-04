using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlantsPageController : MonoBehaviour
{
    [Header("Config")]
    public AssetReference plantPanelAR;
    public Transform plantPanelRoot;
    public ShopSceneController shopSceneController;

    void Start()
    {
        foreach (var plantSData in GameDataManager.instance.plantSDataList)
        {
            var aoh = plantPanelAR.InstantiateAsync(plantPanelRoot);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<PlantPanelController>().Setup(plantSData);
                aoh.Result.GetComponent<PlantPanelController>().OnCraft.AddListener(Craft);
            };
        }
    }

    public void Craft(object plantPanelControllerObj)
    {
        Craft(plantPanelControllerObj as PlantPanelController);
    }

    public void Craft(PlantPanelController plantPanelController)
    {
        shopSceneController.Craft(plantPanelController.plantSData);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Display")]
    public int plantId;
    public PlantSData plantSData;

    [Header("Config")]
    public MI_Growth mI_Groth;
    public SpriteRenderer spriteRenderer;

    [Header("Event")]
    public GameEvent OnNewSpawn;
    public GameEvent OnPointerDown;
    public GameEvent OnPointerUp;

    public void Setup(string plantName, int _plantId, bool newSpawn = false)
    {
        plantId = _plantId;
        Setup(GameDataManager.instance.GetPlantSData(plantName), plantId, newSpawn);
    }

    public void Setup(PlantSData _plantSData, int _plantId, bool newSpawn = false)
    {
        plantId = _plantId;
        plantSData = _plantSData;
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.plantName).Completed += aoh =>
        {
            var plantSprite = aoh.Result as Sprite;
            spriteRenderer.sprite = plantSprite;
            spriteRenderer.transform.localScale = new Vector3(plantSprite.rect.height / plantSprite.rect.width, 1f, 1f);
        };
        
        if (newSpawn)
            NewSpawn();
    }

    public void NewSpawn()
    {
        OnNewSpawn.Invoke(this);
    }

    public void PointerDown()
    {
        OnPointerDown.Invoke(this);
    }

    public void PointerUp()
    {
        OnPointerUp.Invoke(this);
    }
}

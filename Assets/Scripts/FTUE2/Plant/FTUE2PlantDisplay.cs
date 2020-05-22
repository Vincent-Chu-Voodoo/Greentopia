using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PlantDisplay : MonoBehaviour
{
    public int index;
    public SpriteRenderer spriteRenderer;
    public List<Transform> sellableAnchorList;
    public List<GameObject> sellableGOList;

    public void Setup(PlantSData plantSData, int growStage, FTUE2Plant plant)
    {
        ResourceManager.instance.GetPlantSpriteAOH(plantSData.name, growStage).Completed += aoh =>
        {
            var sprite = aoh.Result;
            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.localPosition = Vector3.up * spriteRenderer.sprite.bounds.size.y / 2f;
            plant.ResetBoxSize();
        };
    }

    public void SpawnSellable(PlantSData plantSData)
    {
        var sellable = new GameObject();
        sellableGOList.Add(sellable);
        sellable.transform.parent = sellableAnchorList[index++];
        sellable.transform.localPosition = Vector3.zero;
        var sr = sellable.AddComponent<SpriteRenderer>();
        sr.sortingOrder = 2;
        plantSData.sellable.sellableSprite.LoadAssetAsync<Sprite>().Completed += aoh =>
        {
            sr.sprite = aoh.Result;
        };
    }

    public void CollectSellable()
    {
        var basket = GameObject.FindGameObjectWithTag(TagEnum.Basket.ToString());
        basket.GetComponent<FTUE2Basket>().Collect();
        foreach (var go in sellableGOList)
        {
            var a = go.AddComponent<FTUEApple>();
            a.collectionAnchor = basket.transform;
            a.collectionSpeed = 10f;
        }
        sellableGOList.Clear();
        index = 0;
    }
}

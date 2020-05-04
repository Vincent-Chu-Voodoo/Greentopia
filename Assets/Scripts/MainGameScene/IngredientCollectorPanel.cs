using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCollectorPanel : MonoBehaviour
{
    [Header("Display")]
    public float collectionCount;
    public IngredientCollector ingredientCollector;

    [Header("Config")]
    public Image ingredientImage;
    public TextMeshProUGUI countText;

    [Header("Event")]
    public GameEvent OnCollect;
    public GameEvent OnCollected;

    public void Setup(IngredientCollector _ingredientCollector)
    {
        ingredientCollector = _ingredientCollector;
        var aoh = ResourceManager.instance.GetAtomSpriteAOH(ingredientCollector.collectionType, (int) ingredientCollector.collectionLevel);
        aoh.Completed += _ =>
        {
            ingredientImage.sprite = aoh.Result as Sprite;
        };
    }

    public void Collect(Atom atom, AtomController atomController)
    {
        atomController.RemoveAtom(atom);
        atom.isAnchorToSubGrid = false;
        var mI_Collect = atom.gameObject.AddComponent<MI_Collect>();
        var wp = Camera.main.ScreenToWorldPoint(transform.position + Vector3.forward * 10f);

        mI_Collect.collectionTargetPosition = wp;
        mI_Collect.moveSpeed = 40f;
        mI_Collect.Begin(null);
        mI_Collect.OnEnd.AddListener(Collected);

        OnCollect.Invoke(this);
    }

    public void Collected(object mI_CollectObj)
    {
        var mI_Collect = mI_CollectObj as MI_Collect;
        Destroy(mI_Collect.gameObject);
        collectionCount += 1f;
        countText.SetText($"x{collectionCount:0}");

        OnCollected.Invoke(this);
    }
}

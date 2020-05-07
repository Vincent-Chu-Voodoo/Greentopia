using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainGameHerbariumController : MonoBehaviour
{
    [Header("Display")]
    public List<IngredientData> currentIngredientData;
    public List<MainGamePlantPanel> mainGamePlantPanelList;

    [Header("Config")]
    public Transform mainGamePlantPanelRoot;
    public AssetReference mainGamePlantPanelAR;
    public ScrollTo scrollTo;

    [Header("Event")]
    public GameEvent OnCraft;
    public GameEvent OnCanCraft;

    private void Awake()
    {
        foreach (var plantSData in GameDataManager.instance.plantSDataList)
        {
            if (GameDataManager.instance.GetPrestigeLevel() < plantSData.prestigeLevelRequirement)
                continue;
            var aoh = mainGamePlantPanelAR.InstantiateAsync(mainGamePlantPanelRoot);
            aoh.Completed += _ =>
            {
                aoh.Result.GetComponent<MainGamePlantPanel>().Setup(plantSData);
                aoh.Result.GetComponent<MainGamePlantPanel>().OnCraft.AddListener(Craft);
                aoh.Result.GetComponent<MainGamePlantPanel>().OnIngredientNewSatisfiedAction += IngredientSatisfiedAction;
                mainGamePlantPanelList.Add(aoh.Result.GetComponent<MainGamePlantPanel>());
                scrollTo.To(aoh.Result.GetComponent<MainGamePlantPanel>().GetComponent<RectTransform>());
            };
        }
        
        currentIngredientData = GameDataManager.instance.GenerateIngredientList();
    }

    public void IngredientSatisfiedAction(MainGamePlantPanel mainGamePlantPanel)
    {
        OnCanCraft.Invoke(this);
        //scrollTo.To(mainGamePlantPanel.GetComponent<RectTransform>());
    }

    public void Craft(object mainGamePlantPanelObj)
    {
        OnCraft.Invoke((mainGamePlantPanelObj as MainGamePlantPanel).plantSData);
    }

    public void AtomCombined(object atomObj)
    {
        AtomCombined(atomObj as Atom);
    }

    public void AtomSpawned(object atomObj)
    {
        AtomSpawned(atomObj as Atom);
    }

    public void AtomSpawned(Atom atom)
    {
        //atom.SetCanCraft(GameDataManager.instance.plantSDataList.Exists(i => GameDataManager.instance.GetPrestigeLevel() >= i.prestigeLevelRequirement && i.ingredientList.Exists(j => j.atomEnum == atom.atomType && Mathf.Abs(j.level - atom.atomLevel) < 0.1f)));
    }

    public void AtomCombined(Atom newAtom)
    {
        var destroyedAtomLevel = newAtom.atomLevel - 1;
        var targetIngredientData = currentIngredientData.Find(i => i.atomEnum == newAtom.atomType && Mathf.Abs(i.level - (newAtom.atomLevel - 1f)) < 0.1f);
        if (targetIngredientData is object)
            targetIngredientData.count -= 2;
        if (newAtom.atomLevel >= 4)
        {
            targetIngredientData = currentIngredientData.Find(i => i.atomEnum == newAtom.atomType && Mathf.Abs(i.level - newAtom.atomLevel) < 0.1f);
            if (targetIngredientData is object)
                targetIngredientData.count++;
            else
                currentIngredientData.Add(new IngredientData()
                {
                    atomEnum = newAtom.atomType,
                    level = newAtom.atomLevel,
                    count = 1f
                });
        }
        UpdateIngredient();
        if (newAtom.atomType == AtomEnum.nutrient && newAtom.atomLevel == 2)
            newAtom.SetCanCraft(true);
        if (newAtom.atomType == AtomEnum.cotton && newAtom.atomLevel == 2)
            newAtom.SetCanCraft(true);
        if (newAtom.atomType == AtomEnum.log && newAtom.atomLevel == 2)
            newAtom.SetCanCraft(true);
        //newAtom.SetCanCraft(GameDataManager.instance.plantSDataList.Exists(i => GameDataManager.instance.GetPrestigeLevel() >= i.prestigeLevelRequirement && i.ingredientList.Exists(j => j.atomEnum == newAtom.atomType && Mathf.Abs(j.level - newAtom.atomLevel) < 0.1f)));
    }

    public void UpdateIngredient()
    {
        foreach (var mainGamePlantPanel in mainGamePlantPanelList)
            mainGamePlantPanel.UpdateIngredient(currentIngredientData);
    }
}

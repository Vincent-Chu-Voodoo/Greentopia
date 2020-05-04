using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [Header("Display")]
    public LevelSData levelSData;

    [Header("Config")]
    public AssetReference atomIconDisplayAR;
    public TextMeshProUGUI levelText;
    public Transform atomEnumDisplayRoot;
    public GameObject prestigeUnlockPanel;
    public TextMeshProUGUI prestigeUnlockPanelText;

    [Header("Event")]
    public GameEvent OnClickPlay;

    public void Setup(LevelSData _levelSData, List<AtomDisplaySData> _atomDisplaySDataList, int prestigeLevel)
    {
        prestigeUnlockPanelText.SetText($"Prestige level {_levelSData.prestigeLevel}");
        prestigeUnlockPanel.SetActive(prestigeLevel < _levelSData.prestigeLevel);

        levelSData = _levelSData;
        levelText.SetText($"{_levelSData.level}");

        foreach (var spawnerDetail in _levelSData.spawnerDetailList) {
            var atomSpawnerEnum = spawnerDetail.atomEnum;
            var aoh = atomIconDisplayAR.InstantiateAsync(atomEnumDisplayRoot);
            aoh.Completed += _ =>
            {
                var atomDisplay = aoh.Result;
                var aoh2 = ResourceManager.instance.GetAtomSpriteAOH(atomSpawnerEnum, 1);
                aoh2.Completed += _2 =>
                {
                    atomDisplay.GetComponent<Image>().sprite = aoh2.Result as Sprite;
                };
                // _atomDisplaySDataList.Where(i => i.type == atomSpawnerEnum).FirstOrDefault().spriteList.FirstOrDefault();
            };
        }
    }

    public void Play()
    {
        OnClickPlay.Invoke(this);
    }
}

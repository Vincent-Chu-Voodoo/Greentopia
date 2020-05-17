using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Dev : MonoBehaviour
{
    // 550f
    public float offset;
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            M1(Input.mousePosition);
    }

    public void M1(Vector3 screenPosition)
    {
        
    }

    public void SnapTo()
    {
        Canvas.ForceUpdateCanvases();
        var offset = target.anchoredPosition.x;
        var val = Mathf.Min(offset - offset, 0f);
        print(val);
        contentPanel.anchoredPosition = new Vector2(val, 0f);
    }

    [ContextMenu("M0")]
    public void M0()
    {
        //var allSession = GameDataManager.instance.RefreshSessionDataList();
        //foreach (var session in allSession)
        //{
        //    print($"level {session.level}");
        //    foreach (var gd in session.gridDataList)
        //    {
        //        print($"{gd.atomEnum} {gd.atomLevel}");
        //    }
        //}
    }

    public void OnClick()
    {
        print($"OnClick");
    }
}

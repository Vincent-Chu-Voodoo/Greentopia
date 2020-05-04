using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionCanvas : MonoBehaviour
{
    public float existTime;
    public TextMeshProUGUI versionText;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        versionText.SetText($"v {Application.version}");
        Destroy(gameObject, existTime);
    }
}

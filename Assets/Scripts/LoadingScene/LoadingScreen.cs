using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public float amount;

    [Header("Param")]
    public float estimatedLoadTime;

    [Header("Config")]
    public float loadingBarLength;
    public RectTransform cursorRT;
    public Image loadingBarImage;

    void Update()
    {
        UpdateFill((Mathf.Atan(Time.timeSinceLevelLoad / estimatedLoadTime) * 2f / Mathf.PI));
    }

    public void UpdateFill(float amount)
    {
        loadingBarImage.fillAmount = amount;
        cursorRT.localPosition = Vector3.right * loadingBarLength * amount;
    }

    public void LoadFinished(object loadingSceneControllerObj)
    {
        estimatedLoadTime /= 10f;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FTUEPlantProgress : MonoBehaviour
{
    public float currentTime;
    public float totalTime;

    public TextMeshProUGUI timeText;
    public Image growingBarImage;

    public GameEvent OnGrown;


    void Update()
    {
        currentTime = Mathf.MoveTowards(currentTime, totalTime, Time.deltaTime);
        var remainTime = totalTime - currentTime;
        timeText.SetText($"{remainTime / 3600:0} hr {remainTime % 60:0} min left");
        growingBarImage.fillAmount = currentTime / totalTime;
        if (Mathf.Approximately(currentTime, totalTime))
            OnGrown.Invoke(this);
    }

    public void UseDiamond()
    {
        OnGrown.Invoke(this);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEHandDinAnimation : MonoBehaviour
{
    public float showDelay;

    IEnumerator Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(showDelay);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
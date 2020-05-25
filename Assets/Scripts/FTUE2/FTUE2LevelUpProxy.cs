using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2LevelUpProxy : MonoBehaviour
{
    public GameObject targetGO;

    public void Play(Transform trans)
    {
        targetGO.GetComponent<FTUELevelUp>().startAnchor = trans;
        targetGO.SetActive(true);
    }
}

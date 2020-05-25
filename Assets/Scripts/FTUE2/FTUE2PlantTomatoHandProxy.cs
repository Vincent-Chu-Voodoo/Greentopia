using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2PlantTomatoHandProxy : MonoBehaviour
{
    public GameObject handGO;

    public void Play()
    {
        handGO.SetActive(true);
    }

    public void Stop()
    {
        handGO.SetActive(false);
    }
}

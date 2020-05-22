using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2Basket : MonoBehaviour
{
    public void Collect()
    {
        GetComponent<Animator>().SetTrigger("collect");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2AnchorController : MonoBehaviour
{
    public static int currentIndex;
    public List<Transform> anchorTransList;

    public Transform GetAnchor()
    {
        return anchorTransList[currentIndex++];
    }
}

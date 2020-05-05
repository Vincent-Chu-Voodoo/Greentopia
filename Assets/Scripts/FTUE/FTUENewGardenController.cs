using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUENewGardenController : MonoBehaviour
{
    public static int enterCount;

    public GameEvent OnFirstEnter;
    public GameEvent OnSecondEnter;
    public GameEvent OnPlantTree;

    // Start is called before the first frame update
    void Start()
    {
        switch (enterCount++)
        {
            case 0:
                OnFirstEnter.Invoke(this);
                break;
            case 1:
                OnSecondEnter.Invoke(this);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlantTree()
    {
        OnPlantTree.Invoke(this);
    }
}

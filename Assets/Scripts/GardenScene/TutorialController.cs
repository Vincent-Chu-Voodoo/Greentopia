using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialController : MonoBehaviour
{
    [Header("Param")]
    public int page;

    [Header("Config")]
    public GameObject rootGo;
    public List<GameObject> tutorialList;

    [Header("Config")]
    public GameEvent OnBegin;
    public GameEvent OnProceed;
    public GameEvent OnEnd;

    public GameEvent On1;
    public GameEvent On2;
    public GameEvent On3;

    private void Start()
    {
        if (GameDataManager.instance.gameData.tutorialData.iKnowTheStory)
        {
            Destroy(gameObject);
            return;
        }
        Begin();
    }

    public void Begin()
    {
        rootGo.SetActive(true);
        tutorialList.ForEach(i => i.SetActive(false));
        tutorialList.FirstOrDefault()?.SetActive(true);
        OnBegin.Invoke(this);
        On1.Invoke(this);
    }

    public void Proceed()
    {
        tutorialList[page++].SetActive(false);
        if (page < tutorialList.Count)
        {
            tutorialList[page].SetActive(true);
            OnProceed.Invoke(this);
            if (page == 1)
                On2.Invoke(this);
            else if (page == 2)
                On3.Invoke(this);
        }
        else
        {
            rootGo.SetActive(false);
            GameDataManager.instance.gameData.tutorialData.iKnowTheStory = true;
            OnEnd.Invoke(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2Tutorial : MonoBehaviour
{
    public static int tutorialIndex = 33;

    public GameEvent OnProceed;

    Coroutine proceedWithDelayRoutine;

    protected void Start()
    {
        Proceed();
    }

    public void ProceedWithDelay(float delay)
    {
        StartCoroutine(ProceedWithDelayLoop(delay));
    }

    IEnumerator ProceedWithDelayLoop(float delay)
    {
        yield return new WaitForSeconds(delay);
        Proceed();
    }

    public void Proceed(int index)
    {
        Proceed();
    }

    public void Proceed()
    {
        if (transform.Find($"{tutorialIndex + 1}") is null)
        {
            if (transform.Find($"{tutorialIndex}") is null)
                throw new System.Exception($"No valid tutorial:{tutorialIndex}");
            else
            {
                transform.Find($"{tutorialIndex}")?.gameObject.SetActive(true);
                return;
            }
        }
        transform.Find($"{tutorialIndex}")?.gameObject.SetActive(false);
        tutorialIndex++;
        transform.Find($"{tutorialIndex}")?.gameObject.SetActive(true);
        OnProceed.Invoke(this);
    }

    public void TappedAnyWhere()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i + 1)?.gameObject.SetActive(true);
                break;
            }
        }
    }

    [ContextMenu("Print")]
    public void Print()
    {
        print($"tutorialIndex: {tutorialIndex}");
    }
}

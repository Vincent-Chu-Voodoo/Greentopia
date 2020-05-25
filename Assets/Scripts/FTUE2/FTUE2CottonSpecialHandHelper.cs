using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE2CottonSpecialHandHelper : MonoBehaviour
{
    public float delay;
    public AtomController atomController;
    public Coroutine helperRoutine;

    public void OnAtomCombined(object atomObj)
    {
        var atom = atomObj as Atom;
        if (atom.atomType == AtomEnum.cotton && atom.atomLevel == 3)
        {
            helperRoutine = StartCoroutine(HelperLoop(atom));
        }
        if (atom.atomType == AtomEnum.cotton && atom.atomLevel == 4)
        {
            StopCoroutine(helperRoutine);
            GetComponent<FTUEHandAnimation>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator HelperLoop(Atom cotton3)
    {
        yield return new WaitForSeconds(delay);
        var ha = GetComponent<FTUEHandAnimation>();
        ha.anchorFrom = cotton3.transform;
        ha.anchorTo = atomController.allAtomList.Find(i => i.atomType == AtomEnum.cotton && i.atomLevel == 3 && i.isDusty == true).transform;
        ha.enabled = true;
    }
}

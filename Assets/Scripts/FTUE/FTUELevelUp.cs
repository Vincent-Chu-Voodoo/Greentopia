using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FTUELevelUp : MonoBehaviour
{
    public GameObject bookPrefab;
    public Transform bookRoot;

    public int bookCount;
    public float bookDelay;
    public float bookSpeed;

    public int expIncrease;
    public int expTarget;
    public float expStartDelay;
    public float expDelay;
    public bool dontAddXp;

    public TextMeshProUGUI expText;

    public Transform startAnchor;
    public Transform endAnchor;

    public FTUE2Header header;

    void Start()
    {
        StartCoroutine(BookLoop());
        StartCoroutine(ExpLoop());
    }

    IEnumerator BookLoop()
    {
        for (var i = 0; i < bookCount; i++)
        {
            var newBook = Instantiate(bookPrefab, bookRoot);
            newBook.transform.position = startAnchor.position;
            newBook.GetComponent<FTUEApple>().collectionSpeed = bookSpeed;
            newBook.GetComponent<FTUEApple>().collectionAnchor = endAnchor;
            yield return new WaitForSeconds(bookDelay);
        }
    }

    IEnumerator ExpLoop()
    {
        if (!dontAddXp)
            header.AddXp(100f);
        yield return null;
        //yield return new WaitForSeconds(expStartDelay);
        //var exp = 0f;
        //while (exp < expTarget)
        //{
        //    exp = Mathf.MoveTowards(exp, expTarget, expIncrease);
        //    expText.SetText($"{exp:0}");
        //    yield return new WaitForSeconds(expDelay);
        //}
    }
}

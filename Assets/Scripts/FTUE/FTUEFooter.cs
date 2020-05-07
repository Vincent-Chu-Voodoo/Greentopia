using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class FTUEFooter : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;

    public Coroutine stringRoutine;

    public void ShowText(string compactText)
    {
        var tcText = compactText.Split(new char[] { ';' });
        if (tcText.Length > 1)
            ShowText(tcText[0], tcText[1]);
    }

    public void ShowText(string title, string content)
    {
        titleText.SetText(title);
        if (stringRoutine != null)
            StopCoroutine(stringRoutine);
        stringRoutine = StartCoroutine(ShowTextLoop(content));
    }

    public IEnumerator ShowTextLoop(string targetText)
    {
        var builder = new StringBuilder();
        var index = 0;
        while (builder.Length < targetText.Length)
        {
            builder.Append(targetText[index++]);
            contentText.SetText(builder);
            yield return null;
        }
    }
}

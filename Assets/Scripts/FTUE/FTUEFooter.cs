using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FTUEFooter : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;

    public void ShowText(string compactText)
    {
        var tcText = compactText.Split(new char[] { ';' });
        if (tcText.Length > 1)
            ShowText(tcText[0], tcText[1]);
    }

    public void ShowText(string title, string content)
    {
        titleText.SetText(title);
        contentText.SetText(content);
    }
}

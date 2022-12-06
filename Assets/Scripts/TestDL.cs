using System;
using UnityEngine;

public class TestDL : MonoBehaviour
{
    private void Awake()
    {
        Application.deepLinkActivated += OnDeepLinkActivated;
        if (!string.IsNullOrEmpty(Application.absoluteURL))
            OnDeepLinkActivated(Application.absoluteURL);
    }

    private void OnDeepLinkActivated(string url)
    {
        string[] urlArg = url.Split('?');
        string[] args = new string[0];
        if (urlArg.Length > 1)
        {
            char[] charSeparators = new char[] { '&' };
            args = urlArg[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        for (int i = 0; i < args.Length; i++)
        {
            Debug.Log(args[i]);
        }
    }

}

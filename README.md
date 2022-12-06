# UnityDeepLink

DeepLink 是可以直接用網址呼叫 App 的方式之一，以前有提到可以利用 [Get Android Intent Data for Unity][wenrongIntent] 這邊文章提到的方式，呼叫 App，不過這只限定 Android，iOS 則還是需要用 Deeplink 的方式呼叫。主要是當時 Unity 版本並不支援 DeepLink，所以只能自己寫原生的，才會有之前的[這篇][wenrongIntent]文章，更重要的是，使用之前的呼叫方式是需要某些權限，但目前 Google 也把這些權限關閉，無法正常上架需要自己寫信去解釋才會願意讓你正常上架。所以建議是棄用這種方法改用 Deeplink。

## 詳細資料

* [Unity Deep Link][unitydl]

* [Android Deep Link][dl_android]

* [iOS Deep Link][dl_ios]

建議是一定要把 [Unity Deep Link][unitydl] 看完，才會知道怎麼設定，其餘兩邊則是原生地設定方式，可以參考。

## 使用方式

[Unity Deep link Doc][unitydl]

官方也有文件解釋 Deep link 的基礎設定。

## Script Start

需要再被喚醒 app 的瞬間也就是 Awake 時，先讀取 `Application.absoluteURL` 才能讀取道 deep link 的資料。`Application.deepLinkActivated` 部分則是 app 的 deep link feedback。

```C#
    private void Awake()
    {
        Application.deepLinkActivated += OnDeepLinkActivated;
        if (!string.IsNullOrEmpty(Application.absoluteURL))
            OnDeepLinkActivated(Application.absoluteURL);
    }
```
## Script Url Arg

拆解 Deep link 夾帶的參數，格式大概與 web 的 url get 類似，可以用這種方式去解析，夾帶的參數。

```C#
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
```

________________________________________________________________________________

[unitydl]:https://docs.unity3d.com/Manual/deep-linking.html
[unitydl_android]:https://docs.unity3d.com/Manual/deep-linking-android.html
[unitydl_ios]:https://docs.unity3d.com/Manual/deep-linking-ios.html
[wenrongIntent]:https://wenrongdev.com/posts/get-android-intent-data-for-unity/
[dl_android]:https://developer.android.com/training/app-links/deep-linking
[dl_ios]:https://developer.apple.com/documentation/xcode/allowing-apps-and-websites-to-link-to-your-content

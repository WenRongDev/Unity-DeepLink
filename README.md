# UnityDeepLink

DeepLink 是可以直接用網址呼叫 App 的方式之一，以前有提到可以利用 [Get Android Intent Data for Unity][wenrongIntent] 這邊文章提到的方式，呼叫 App，不過這只限定 Android，iOS 則還是需要用 Deeplink 的方式呼叫。主要是當時 Unity 版本並不支援 DeepLink，所以只能自己寫原生的，才會有之前的[這篇][wenrongIntent]文章，更重要的是，使用之前的呼叫方式是需要某些權限，但目前 Google 也把這些權限關閉，無法正常上架需要自己寫信去解釋才會願意讓你正常上架。所以建議是棄用這種方法改用 Deeplink。

## 詳細資料

* [Unity Deep Link][unitydl]

* [Android Deep Link][dl_android]

* [IOS Deep linking: URL Scheme vs Universal Links][dl_ios]

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

## link Note

deep link 可以用網址來當 link id，類似像手機點開 Youtube 網址時，假如裝置內有 Youtube App 就會自動開啟 App，並且切換至該影片內容。而 deep link 也可以用網址來當 link id 達到這樣的效果。

也可以用來呼叫 app 時，假如該裝置沒有安裝可以直接轉移到 app store上面讓使用者直接下載該 app。

### Android

需要在 `AndroidManifest` 上寫上 link id，可以根據 [Create Deep Links to App Content][dl_android] 參考詳細的設置方式。

* url 呼叫方式

```xml
<intent-filter>
    <action android:name="android.intent.action.VIEW" />
    <category android:name="android.intent.category.DEFAULT" />
    <category android:name="android.intent.category.BROWSABLE" />
    <data android:scheme="http" />
    <data android:scheme="https" />
    <data android:host="wenrongdev.com" />
</intent-filter>
```

可以利用這種方式，app 超連結開啟或者網頁輸入 `https://wenrongdev.com/`、`http://wenrongdev.com/` 時就會自動對應到 App。

* 自訂 id

```xml
<intent-filter>
    <action android:name="android.intent.action.VIEW" />
    <category android:name="android.intent.category.DEFAULT" />
    <category android:name="android.intent.category.BROWSABLE" />
    <data android:scheme="app" />
    <data android:host="wenrongdev" />
</intent-filter>
```

可以利用這種方式，app 超連結開啟或者網頁輸入 `app://wenrongdev` 時就會自動對應到 App。

### iOS

Unity 設定 link id 的方式在 [Deep linking on iOS][unitydl_ios] 有介紹如何設定，而 Xcode 也能設定，不過建議是在 Unity 中設定非必要不建議額外自己在手動設置，主要是怕輸出時忘記導致功能失效。

Xcode 詳細設定可以參考這邊 [IOS Deep linking: URL Scheme vs Universal Links][dl_ios]

![img_1]

這樣設定後就會與 Android 一樣的功能，在瀏覽器輸出該 link id 就會自動對應到 app。
________________________________________________________________________________

[unitydl]:https://docs.unity3d.com/Manual/deep-linking.html
[unitydl_android]:https://docs.unity3d.com/Manual/deep-linking-android.html
[unitydl_ios]:https://docs.unity3d.com/Manual/deep-linking-ios.html
[wenrongIntent]:https://wenrongdev.com/posts/get-android-intent-data-for-unity/
[dl_android]:https://developer.android.com/training/app-links/deep-linking
[dl_ios]:https://medium.com/wolox/ios-deep-linking-url-scheme-vs-universal-links-50abd3802f97
[img_1]:https://imgur.com/WIvC4gC.png

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class NativeShare : MonoBehaviour
{
    public Button buttonShare;

    [Header("RU")]
    public string textPostRu = "Я создал это в приложении ";
    public string textWindowRu = "Куда отправить?";

    [Header("EN")]
    public string textPostEn = "I created this in the application ";
    public string textWindowEn = "Where to send?";

    [Header("Other")]
    public string gameName;

    private bool isProcessing = false;

    private void Awake()
    {
        buttonShare.onClick.AddListener(ButtonShare);
    }

    public void ButtonShare()
    {
        if (!isProcessing)
        {
#if UNITY_ANDROID
            StartCoroutine(ShareAndriod());
#elif UNITY_IOS
            StartCoroutine(ShareIOS());
#endif
        }
    }

#if UNITY_ANDROID
    IEnumerator ShareAndriod()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("screenshot.png", 2);
        string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            string curTextPost = "";
            if (Application.systemLanguage == SystemLanguage.Russian)
                curTextPost = textPostRu;
            else
                curTextPost = textPostEn;

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), curTextPost + gameName);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            string curTextWindow = "";
            if (Application.systemLanguage == SystemLanguage.Russian)
                curTextWindow = textWindowRu;
            else
                curTextWindow = textWindowEn;

            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, curTextWindow);
            currentActivity.Call("startActivity", chooser);

            yield return new WaitForSecondsRealtime(1);
        }
        isProcessing = false;
    }
#endif

#if UNITY_IOS
    IEnumerator ShareIOS()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("screenshot.png", 2);
        string screenShotPath = Path.Combine(Application.persistentDataPath, "screenshot.png");

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
            CallSocialShareAdvanced("", "", "", screenShotPath);

            yield return new WaitForSecondsRealtime(1);
        }

        isProcessing = false;
    }

    public struct ConfigStruct
    {
        public string title;
        public string message;
    }

    [DllImport("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

    public struct SocialSharingStruct
    {
        public string text;
        public string url;
        public string image;
        public string subject;
    }

    [DllImport("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

    public static void CallSocialShare(string title, string message)
    {
        ConfigStruct conf = new ConfigStruct();
        conf.title = title;
        conf.message = message;
        showAlertMessage(ref conf);
    }

    public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
    {
        SocialSharingStruct conf = new SocialSharingStruct();
        conf.text = defaultTxt;
        conf.url = url;
        conf.image = img;
        conf.subject = subject;

        showSocialSharing(ref conf);
    }
#endif
}

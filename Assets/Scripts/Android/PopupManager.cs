using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private const string packName = "com.example.modulo1";
    private const string loggerClassName = "PopupManager";

    private static AndroidJavaClass PopupManagerClass = null;
    private static AndroidJavaObject PopupManagerInstance = null;

    private string title = "Jump Ball ";
    private string message = "Nasheeeee";
    private string button1 = "close";

    class AlertViewCallBack : AndroidJavaProxy
    {
        private System.Action<int> alertHandler = null;

        public AlertViewCallBack(System.Action<int> alertHandlerIn) : base(packName + "." + loggerClassName + "$AlertViewCallBack")
        {
            alertHandler = alertHandlerIn;
        }

        public void OnButtonTapped(int index)
        {
            Debug.Log("Button tapped: " + index);
            alertHandler?.Invoke(index);
        }
    }

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        PopupManagerClass = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        PopupManagerClass.SetStatic("mainActivity", activity);

        PopupManagerInstance = PopupManagerClass.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }

    public void ShowPopup()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (PopupManagerInstance == null)
        {
            Init();
        }
        ShowAlertDialog(new string[] { title + Application.version, message, button1 }, (int obj) =>
        {
            Debug.Log("Local Handler called: " + obj);
        });
#endif
    }

    private void ShowAlertDialog(string[] strings, System.Action<int> handler = null)
    {
        if (strings.Length < 3)
        {
            Debug.LogError("AlertView requires at least 3 strings");
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            PopupManagerInstance?.Call("ShowAlertView", new object[] { strings, new AlertViewCallBack(handler)});
        }
        else
        {
            Debug.LogWarning("AlertView not supported on this platform");
        }
    }
}

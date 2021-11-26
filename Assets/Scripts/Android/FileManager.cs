using UnityEngine;

public class FileManager : MonoBehaviour
{
    private const string packName = "com.example.modulo1";
    private const string loggerClassName = "FileManager";

    private static AndroidJavaClass FileManagerClass = null;
    private static AndroidJavaObject FileManagerInstance = null;

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        FileManagerClass = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        FileManagerClass.SetStatic("mainActivity", activity);

        FileManagerInstance = FileManagerClass.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }

    public static string ReadFile()
    {
        if (FileManagerInstance == null)
        {
            Init();
        }
        return FileManagerInstance?.Call<string>("ReadFile");
    }

    public static void WriteFile(string data)
    {
        if (FileManagerInstance == null)
        {
            Init();
        }
        FileManagerInstance?.Call("WriteFile", data);
    }
}

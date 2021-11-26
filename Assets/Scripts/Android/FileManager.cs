using UnityEngine;

public class FileManager : MonoBehaviourSingleton<FileManager>
{
    private const string packName = "com.example.modulo1";
    private const string loggerClassName = "FileManager";

    private AndroidJavaClass FileManagerClass = null;
    private AndroidJavaObject FileManagerInstance = null;

    private void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        FileManagerClass = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        FileManagerClass.SetStatic("mainActivity", activity);

        FileManagerInstance = FileManagerClass.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }

    public string ReadFile()
    {
        if (FileManagerInstance == null)
        {
            Init();
        }
        return FileManagerInstance?.Call<string>("ReadFile");
    }

    public void WriteFile(string data)
    {
        if (FileManagerInstance == null)
        {
            Init();
        }
        FileManagerInstance?.Call("WriteFile", data);
    }
}

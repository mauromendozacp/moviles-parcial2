using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLogger
{
    private const string packName = "com.example.modulo1";
    private const string loggerClassName = "MLogger";

    private static AndroidJavaClass MLoggerClass = null;
    private static AndroidJavaObject MLoggerInstance = null;

    private static void Init()
    {
        MLoggerClass = new AndroidJavaClass(packName + "." + loggerClassName);
        MLoggerInstance = MLoggerClass.CallStatic<AndroidJavaObject>("GetInstance");
    }

    public static void SendLog(string msg)
    {
        if (MLoggerInstance == null)
        {
            Init();
        }
        MLoggerInstance?.Call("SendLog", msg);
    }
}

using System;
using UnityEngine;
using UnityEngine.Android;

public sealed class AndroidNativeLogger : IDisposable
{
    private const string MANAGER_CLASS_NAME = "com.example.brizuelalogger.BrizuelaLoggerManager";
    private const string GET_INSTANCE_METHOD = "getInstance";
    private const string INIT_METHOD = "init";
    private const string SEND_LOG_METHOD = "sendLog";
    private const string GET_LOGS_METHOD = "getLogs";
    private const string CLEAR_LOGS_METHOD = "clearLogs";

    private readonly AndroidJavaObject manager;

    public AndroidNativeLogger()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        using (AndroidJavaClass managerClass = new AndroidJavaClass(MANAGER_CLASS_NAME))
        {
            manager = managerClass.CallStatic<AndroidJavaObject>(GET_INSTANCE_METHOD);
        }

        manager.Call(INIT_METHOD, AndroidApplication.currentActivity);
        Application.logMessageReceived += OnLogMessageReceived;
    }

    private void OnLogMessageReceived(string message, string stackTrace, LogType type)
    {
        bool isError = type == LogType.Error || type == LogType.Exception || type == LogType.Assert;

        if (isError && !string.IsNullOrEmpty(stackTrace))
            SendLog(type, $"{message}\n{stackTrace.TrimEnd()}");
        else
            SendLog(type, message);
    }

    public void SendLog(LogType type, string message)
    {
        if (manager == null)
            return;

        manager.Call(SEND_LOG_METHOD, type.ToString(), message ?? string.Empty);
    }

    public string GetLogs()
    {
        if (manager == null)
            return string.Empty;

        return manager.Call<string>(GET_LOGS_METHOD) ?? string.Empty;
    }

    public void ClearLogs()
    {
        if (manager == null)
            return;

        manager.Call(CLEAR_LOGS_METHOD, AndroidApplication.currentActivity);
    }

    public void Dispose()
    {
        if (manager == null)
            return;

        Application.logMessageReceived -= OnLogMessageReceived;
        manager.Dispose();
    }
}
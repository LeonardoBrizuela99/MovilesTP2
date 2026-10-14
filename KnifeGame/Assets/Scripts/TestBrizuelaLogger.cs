using UnityEngine;

public class TestBrizuelaLogger : MonoBehaviour
{

#if UNITY_ANDROID

    AndroidJavaObject loggerManagerInstance;
    void Start()
    {
        using var loggerClass = new AndroidJavaClass("com.example.brizuelalogger.BrizuelaLoggerManager");
        loggerManagerInstance = loggerClass.CallStatic<AndroidJavaObject>("getInstance");
        loggerManagerInstance.Call("sendLong", "Hola desde Unity");
        long t = loggerManagerInstance.Call<long>("getTime");
        Debug.Log($"Hora nativa: {t}");
    }
    void OnDestroy() =>loggerManagerInstance?.Dispose();

#endif
}
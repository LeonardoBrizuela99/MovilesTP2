using UnityEngine;

public class LoggerManager : MonoBehaviour
{
    public static LoggerManager instance;
    private AndroidNativeLogger logger;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        logger = new AndroidNativeLogger();
        DontDestroyOnLoad(gameObject);
    }

    public string GetLogs() => logger.GetLogs();

    public void ClearLogs() => logger.ClearLogs();

    public void SendLog(LogType logType, string message) => logger.SendLog(logType, message);

    private void OnDestroy()
    {
        if (instance == this) logger?.Dispose();
    }
}
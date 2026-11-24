using TMPro;
using UnityEngine;

public class LoggerScreen : MonoBehaviour
{
    [SerializeField] TMP_Text logText;

    private void OnEnable()
    {
        string logs = LoggerManager.instance != null ? LoggerManager.instance.GetLogs() : "";
        ShowLogs(logs);
    }

    public void ClearLogs()
    {
        if (LoggerManager.instance != null)
            LoggerManager.instance.ClearLogs();
        ShowLogs("");
    }

    private void ShowLogs(string logs)
    {
        if (logs.Length == 0)
            logText.text = " ";
        else
            logText.text = logs;
    }
}
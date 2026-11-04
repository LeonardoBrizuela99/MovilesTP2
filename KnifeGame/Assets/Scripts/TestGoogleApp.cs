using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesDiagnostics : MonoBehaviour
{
    private string _status = "Authenticate no llamado";

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        _status = "Esperando callback...";
        PlayGamesPlatform.Instance.Authenticate(OnAuthenticated);
    }

    private void OnAuthenticated(SignInStatus status)
    {
        _status = status == SignInStatus.Success
            ? $"Success: {PlayGamesPlatform.Instance.GetUserDisplayName()}"
            : status.ToString();
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 48;
        GUI.skin.button.fontSize = 48;
        GUI.Label(new Rect(40, 40, Screen.width - 80, 400), _status);

        if (GUI.Button(new Rect(40, 460, 600, 140), "Login manual"))
        {
            _status = "Esperando callback manual...";
            PlayGamesPlatform.Instance.ManuallyAuthenticate(OnAuthenticated);
        }
    }
}

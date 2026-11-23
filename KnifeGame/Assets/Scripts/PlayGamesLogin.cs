using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesLogin : MonoBehaviour
{
    static bool started;

    private void Start()
    {
        if (started || Application.isEditor) return;
        started = true;

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(OnAuthenticated);
    }

    void OnAuthenticated(SignInStatus status)
    {
        if (status == SignInStatus.Success)
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_primer_inicio, ok => { });
        else
            Debug.Log("Google Play Games sign in: " + status);
    }
}
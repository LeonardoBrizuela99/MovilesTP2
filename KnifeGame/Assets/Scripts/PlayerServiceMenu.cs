using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayServicesMenu : MonoBehaviour
{
    public void ShowAchievements()
    {
        Run(() => PlayGamesPlatform.Instance.ShowAchievementsUI());
    }

    public void ShowLeaderboard()
    {
        Run(() => PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highest_level));
    }

    void Run(System.Action action)
    {
        if (Application.isEditor)
        {
            Debug.Log("Google Play Games UI only works on a device.");
            return;
        }

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            action();
            return;
        }

        PlayGamesPlatform.Instance.ManuallyAuthenticate(status =>
        {
            if (status == SignInStatus.Success) action();
        });
    }
}
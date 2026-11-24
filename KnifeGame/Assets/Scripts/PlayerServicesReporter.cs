using GooglePlayGames;
using UnityEngine;
public class PlayServicesReporter : MonoBehaviour
{
    const int KnivesForMaster = 25;
    const int ApplesForCollector = 10;

    int knivesStuckThisSession;

    private void Start()
    {
        GameEvents.current.OnLevelWon += HandleLevelWon;
        GameEvents.current.OnGameCompleted += HandleGameCompleted;
        GameEvents.current.OnKnifeStuck += HandleKnifeStuck;
        GameEvents.current.OnApplesChanged += HandleApplesChanged;
    }

    private void OnDestroy()
    {
        if (GameEvents.current == null) return;
        GameEvents.current.OnLevelWon -= HandleLevelWon;
        GameEvents.current.OnGameCompleted -= HandleGameCompleted;
        GameEvents.current.OnKnifeStuck -= HandleKnifeStuck;
        GameEvents.current.OnApplesChanged -= HandleApplesChanged;
    }

    bool Ready => PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();

    void HandleLevelWon(int levelIndex)
    {
        if (!Ready) return;

        PlayGamesPlatform.Instance.ReportScore(levelIndex + 1, GPGSIds.leaderboard_highest_level, ok => { });

        
        if (levelIndex == 0)
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_first_level, ok => { });
        Debug.Log("Achievement unlocked");
    }

    void HandleGameCompleted()
    {
        if (!Ready) return;
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_game_complete, ok => { });
        Debug.Log("Achievement unlocked");
    }

    void HandleKnifeStuck()
    {
        knivesStuckThisSession++;
        if (!Ready) return;

        if (knivesStuckThisSession == KnivesForMaster)
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_knife_master, ok => { });
        Debug.Log("Achievement unlocked");
    }

    void HandleApplesChanged(int totalApples)
    {
        if (!Ready) return;

        if (totalApples >= ApplesForCollector)
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_apple_collector, ok => { });
        Debug.Log("Achievement unlocked");
    }
}
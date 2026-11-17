using GooglePlayGames;
using UnityEngine;

public class PlayServicesReporter : MonoBehaviour
{
    int knivesStuckThisSession;

    private void Start()
    {
        GameEvents.current.OnLevelWon += HandleLevelWon;
        GameEvents.current.OnKnifeStuck += HandleKnifeStuck;
    }

    private void OnDestroy()
    {
        if (GameEvents.current == null) return;
        GameEvents.current.OnLevelWon -= HandleLevelWon;
        GameEvents.current.OnKnifeStuck -= HandleKnifeStuck;
    }

    bool Ready => PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();

    void HandleLevelWon(int levelIndex)
    {
        if (!Ready) return;

      
    }

    void HandleKnifeStuck()
    {
        knivesStuckThisSession++;
        if (!Ready) return;
        
    }
}
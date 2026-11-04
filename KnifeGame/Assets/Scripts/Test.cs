using GooglePlayGames;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SendFirstAchievement();
    }

    public void SendFirstAchievement()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_primer_inicio,
            success => { Debug.Log("IncrementalAchievement: " + success); });
        Debug.Log(GPGSIds.achievement_primer_inicio);
    }
}
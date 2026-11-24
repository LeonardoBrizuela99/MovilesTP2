using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] TMP_Text knivesText;
    [SerializeField] TMP_Text applesText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject losePanel;
    [SerializeField] TMP_Text retryText;        
    [SerializeField] GameObject completedPanel;

    private void Start()
    {
        GameEvents.current.OnKnivesLeftChanged += UpdateKnives;
        GameEvents.current.OnApplesChanged += UpdateApples;
        GameEvents.current.OnLevelStarted += LevelStarted;
        GameEvents.current.OnLevelLost += ShowLose;
        GameEvents.current.OnRetryCountdown += UpdateCountdown;
        GameEvents.current.OnGameCompleted += ShowCompleted;
    }

    private void OnDestroy()
    {
        if (GameEvents.current == null) return;
        GameEvents.current.OnKnivesLeftChanged -= UpdateKnives;
        GameEvents.current.OnApplesChanged -= UpdateApples;
        GameEvents.current.OnLevelStarted -= LevelStarted;
        GameEvents.current.OnLevelLost -= ShowLose;
        GameEvents.current.OnRetryCountdown -= UpdateCountdown;
        GameEvents.current.OnGameCompleted -= ShowCompleted;
    }

    void UpdateKnives(int left) => knivesText.text = left.ToString();
    void UpdateApples(int total) => applesText.text = total.ToString();

    void LevelStarted(int index)
    {
        levelText.text = $"Level {index + 1}";
        losePanel.SetActive(false);
        completedPanel.SetActive(false);
    }

    void ShowLose() => losePanel.SetActive(true);

    void UpdateCountdown(int seconds)
    {
        if (retryText != null)
            retryText.text = $"Level restarting in {seconds}...";
    }

    void ShowCompleted() => completedPanel.SetActive(true);
}
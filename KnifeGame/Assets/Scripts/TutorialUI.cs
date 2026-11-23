using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text messageText;
    [SerializeField] float stepDuration = 2.5f;

    readonly string[] steps =
    {
        "Tap anywhere to throw a knife",
        "Don't hit the other knives!",
        "Hit apples to earn coins for the shop"
    };

    private void Start()
    {
        if (SaveSystem.TutorialDone)
        {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);
        messageText.text = steps[0];
        GameEvents.current.OnKnifeThrown += HandleFirstThrow;
    }

    private void OnDestroy()
    {
        if (GameEvents.current != null)
            GameEvents.current.OnKnifeThrown -= HandleFirstThrow;
    }

    void HandleFirstThrow()
    {
        GameEvents.current.OnKnifeThrown -= HandleFirstThrow;
        StartCoroutine(ShowRemainingSteps());
    }

    IEnumerator ShowRemainingSteps()
    {
        for (int i = 1; i < steps.Length; i++)
        {
            messageText.text = steps[i];
            yield return new WaitForSeconds(stepDuration);
        }

        panel.SetActive(false);
        SaveSystem.TutorialDone = true;
    }
}
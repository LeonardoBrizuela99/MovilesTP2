using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current { get; private set; }

    public event Action OnKnifeStuck;              
    public event Action OnKnifeHitKnife;          
    public event Action OnAppleHit;               
    public event Action<int> OnKnivesLeftChanged;  
    public event Action<int> OnApplesChanged;     
    public event Action<int> OnLevelStarted;      
    public event Action<int> OnLevelWon;           
    public event Action OnLevelLost;
    public event Action OnGameCompleted;          

    private void Awake()
    {
        current = this;
    }

    private void OnDestroy()
    {
        if (current == this) current = null;
    }

    public void KnifeStuck() => OnKnifeStuck?.Invoke();
    public void KnifeHitKnife() => OnKnifeHitKnife?.Invoke();
    public void AppleHit() => OnAppleHit?.Invoke();
    public void KnivesLeftChanged(int left) => OnKnivesLeftChanged?.Invoke(left);
    public void ApplesChanged(int total) => OnApplesChanged?.Invoke(total);
    public void LevelStarted(int index) => OnLevelStarted?.Invoke(index);
    public void LevelWon(int index) => OnLevelWon?.Invoke(index);
    public void LevelLost() => OnLevelLost?.Invoke();
    public void GameCompleted() => OnGameCompleted?.Invoke();
}
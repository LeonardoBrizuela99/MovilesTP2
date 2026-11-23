using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelData[] levels;
    [SerializeField] Log log;
    [SerializeField] KnifeThrower thrower;
    [SerializeField] Apple applePrefab;
    [SerializeField] float nextLevelDelay = 1.2f;
    [SerializeField] float loseRetryDelay = 3f;

    public int LevelIndex { get; private set; }
    public int Apples { get; private set; }
    public LevelData CurrentLevel => levels[LevelIndex];

    GameState state;
    int stuckCount;
    readonly List<Apple> spawnedApples = new List<Apple>();

    private void Start()
    {
        Apples = SaveSystem.Apples;
        GameEvents.current.OnKnifeStuck += HandleKnifeStuck;
        GameEvents.current.OnKnifeHitKnife += HandleKnifeHitKnife;
        GameEvents.current.OnAppleHit += HandleAppleHit;
        GameEvents.current.ApplesChanged(Apples);

        StartLevel(0);
    }

    private void OnDestroy()
    {
        if (GameEvents.current == null) return;
        GameEvents.current.OnKnifeStuck -= HandleKnifeStuck;
        GameEvents.current.OnKnifeHitKnife -= HandleKnifeHitKnife;
        GameEvents.current.OnAppleHit -= HandleAppleHit;
    }

    public void StartLevel(int index)
    {
        LevelIndex = index;
        ChangeState(new PlayingState(this));
    }

    public void Retry() => StartLevel(LevelIndex);

    void HandleKnifeStuck() => state?.OnKnifeStuck();
    void HandleKnifeHitKnife() => state?.OnKnifeHitKnife();

    void HandleAppleHit()
    {
        Apples++;
        SaveSystem.Apples = Apples;
        GameEvents.current.ApplesChanged(Apples);
    }

    void ChangeState(GameState newState)
    {
        state?.Exit();
        state = newState;
        state.Enter();
    }

    void Vibrate()
    {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    void SetupLevelObjects()
    {
        foreach (Apple a in spawnedApples) if (a != null) Destroy(a.gameObject);
        spawnedApples.Clear();

        LevelData lvl = CurrentLevel;
        int total = lvl.preStuckKnives + lvl.apples;
        if (total == 0) return;

        float spacing = 360f / total;
        float offset = Random.Range(0f, 360f);

        for (int i = 0; i < total; i++)
        {
            float angle = offset + i * spacing;
            if (i < lvl.preStuckKnives)
            {
                Knife k = thrower.GetKnife();
                k.ResetKnife(Vector3.zero);
                log.Attach(k.transform, angle, log.KnifeStickDistance);
                k.SetAsStuck();
            }
            else
            {
                Apple a = Instantiate(applePrefab);
                log.Attach(a.transform, angle, log.AppleDistance);
                spawnedApples.Add(a);
            }
        }
    }

    abstract class GameState
    {
        protected readonly GameManager gm;
        protected GameState(GameManager gm) { this.gm = gm; }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void OnKnifeStuck() { }
        public virtual void OnKnifeHitKnife() { }
    }

    class PlayingState : GameState
    {
        public PlayingState(GameManager gm) : base(gm) { }

        public override void Enter()
        {
            gm.stuckCount = 0;
            gm.log.Setup(gm.CurrentLevel);
            gm.thrower.BeginLevel(gm.CurrentLevel.knivesToThrow);
            gm.SetupLevelObjects();
            GameEvents.current.LevelStarted(gm.LevelIndex);
        }

        public override void OnKnifeStuck()
        {
            gm.stuckCount++;
            if (gm.stuckCount >= gm.CurrentLevel.knivesToThrow)
                gm.ChangeState(new WonState(gm));
        }

        public override void OnKnifeHitKnife()
        {
            gm.ChangeState(new LostState(gm));
        }
    }

    class WonState : GameState
    {
        public WonState(GameManager gm) : base(gm) { }

        public override void Enter()
        {
            gm.thrower.StopThrowing();
            gm.log.Stop();

            int next = gm.LevelIndex + 1;
            if (next > SaveSystem.MaxLevel && next < gm.levels.Length)
                SaveSystem.MaxLevel = next;

            GameEvents.current.LevelWon(gm.LevelIndex);
            gm.StartCoroutine(GoNext(next));
        }

        IEnumerator GoNext(int next)
        {
            yield return new WaitForSeconds(gm.nextLevelDelay);
            if (next >= gm.levels.Length)
                GameEvents.current.GameCompleted();
            else
                gm.StartLevel(next);
        }
    }

    class LostState : GameState
    {
        public LostState(GameManager gm) : base(gm) { }

        public override void Enter()
        {
            gm.thrower.StopThrowing();
            gm.log.Stop();
            gm.Vibrate();
            GameEvents.current.LevelLost();
            gm.StartCoroutine(RetryAfterDelay());
        }

        IEnumerator RetryAfterDelay()
        {
            yield return new WaitForSeconds(gm.loseRetryDelay);
            gm.Retry();
        }
    }
}
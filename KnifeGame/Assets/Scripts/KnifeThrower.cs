using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class KnifeThrower : MonoBehaviour
{
    [SerializeField] Knife knifePrefab;
    [SerializeField] Transform spawnPoint;     
    [SerializeField] float throwCooldown = 0.12f;
    [SerializeField] InputActionReference throwAction;

    ObjectPool<Knife> pool;
    readonly List<Knife> activeKnives = new List<Knife>();

    Knife currentKnife;
    int knivesLeft;
    float lastThrowTime;
    bool canThrow;
    private void OnEnable() => throwAction.action.Enable();
    private void OnDisable() => throwAction.action.Disable();
    private void Awake()
    {
        pool = new ObjectPool<Knife>(
            createFunc: () => Instantiate(knifePrefab),
            actionOnGet: k => k.gameObject.SetActive(true),
            actionOnRelease: k => k.gameObject.SetActive(false),
            actionOnDestroy: k => Destroy(k.gameObject),
            collectionCheck: false,
            defaultCapacity: 12,
            maxSize: 40);
    }

    public void BeginLevel(int knives)
    {
        ReleaseAll();
        knivesLeft = knives;
        canThrow = true;
        SpawnNext();
        GameEvents.current.KnivesLeftChanged(knivesLeft);
    }

    public void StopThrowing() => canThrow = false;

    public Knife GetKnife()
    {
        Knife k = pool.Get();
        activeKnives.Add(k);
        return k;
    }

    public void ReleaseAll()
    {
        foreach (Knife k in activeKnives) pool.Release(k);
        activeKnives.Clear();
        currentKnife = null;
    }

    private void SpawnNext()
    {
        if (knivesLeft <= 0) { currentKnife = null; return; }
        currentKnife = GetKnife();
        currentKnife.ResetKnife(spawnPoint.position);
    }

    private void Update()
    {
        if (!canThrow || currentKnife == null) return;

        
        if (!throwAction.action.WasPressedThisFrame()) return;
        if (Time.time - lastThrowTime < throwCooldown) return;

        currentKnife.Throw();
        GameEvents.current.KnifeThrown();
        currentKnife = null;
        knivesLeft--;
        lastThrowTime = Time.time;
        GameEvents.current.KnivesLeftChanged(knivesLeft);
        SpawnNext();
    }
}
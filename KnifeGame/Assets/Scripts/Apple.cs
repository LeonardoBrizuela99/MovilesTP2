using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] ParticleSystem collectFxPrefab;

    public void Collect()
    {
        if (collectFxPrefab != null)
            Instantiate(collectFxPrefab, transform.position, Quaternion.identity);

        GameEvents.current.AppleHit();
        gameObject.SetActive(false);
    }
}
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip knifeHitClip;
    [SerializeField] AudioSource audioSource;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameEvents.current.OnKnifeStuck += PlayKnifeHit;
    }

    private void OnDestroy()
    {
        if (GameEvents.current != null)
            GameEvents.current.OnKnifeStuck -= PlayKnifeHit;
    }

    void PlayKnifeHit()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f); 
        audioSource.PlayOneShot(knifeHitClip);
    }
}
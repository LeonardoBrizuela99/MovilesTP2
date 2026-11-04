using UnityEngine;

public class KnifeFactory : MonoBehaviour
{
    [SerializeField] GameObject knife;

    private void Start()
    {
        GameEvents.current.onKnifeCollision += OnKnifeCollider;
    }

    private void OnKnifeCollider()
    {
        GameObject.Instantiate(knife);
        Debug.Log("se creo un cuchillo");
    }
}

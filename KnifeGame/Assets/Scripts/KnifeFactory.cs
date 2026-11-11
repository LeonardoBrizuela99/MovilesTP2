using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class KnifeFactory : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] Spin wheel;
    [SerializeField]Transform pos;

    private void Start()
    {
        GameEvents.current.onKnifeCollision += OnKnifeCollider;
    }

    private void OnKnifeCollider()
    {
        if (wheel.knifeCount<=4)
        {
         GameObject.Instantiate(knife,pos.position,Quaternion.identity);
         Debug.Log("se creo un cuchillo");

        }
    }
   
}

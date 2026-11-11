using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] GameObject wheel;
    [SerializeField] Rigidbody2D wheelRigidbody;
    [SerializeField] public int knifeCount = 0;
    void Start()
    {
       // wheelRigidbody.AddTorque(120f);
        GameEvents.current.onKnifeWin += OnKnifeWin;
    }
    private void FixedUpdate()
    {
        wheelRigidbody.angularVelocity = 120;
        
    }
    private void OnKnifeWin()
    {
        knifeCount++;

        if (knifeCount == 4)
        {
            Debug.Log("Ganaste");
        }

    }
}

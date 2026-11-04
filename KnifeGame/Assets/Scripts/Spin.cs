using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] GameObject wheel;
    [SerializeField] Rigidbody2D wheelRigidbody;
    void Start()
    {
        wheelRigidbody.AddTorque(360f); 
    }
}

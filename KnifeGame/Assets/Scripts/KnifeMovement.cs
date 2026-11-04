using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] Rigidbody2D rb;
    private bool dontEvent;

    private void Update()
    {

    }
    private void OnNewaction()
    {
        rb.AddForce(new Vector3(0, 100, 0));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(collision.transform);
        if (dontEvent == false)
        {
         dontEvent = true;
        GameEvents.current.OnKnifeCollision();
        }
    }
}
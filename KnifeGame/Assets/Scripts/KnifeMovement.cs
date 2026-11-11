using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] GameObject knife;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    private bool dontEvent;

    private void Update()
    {

    }
    private void OnNewaction()
    {
        rb.AddForce(new Vector3(0, 180, 0));
        Debug.Log("On new action");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(collision.transform);

        if (dontEvent == false)
        {
            dontEvent = true;
            GameEvents.current.OnKnifeCollision();
            col.enabled = false;
        }
    }

    private void Awake()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = Vector3.one;
    }
}
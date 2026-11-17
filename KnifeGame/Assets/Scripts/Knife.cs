using UnityEngine;

public class Knife : MonoBehaviour
{
    public enum State { Idle, Flying, Stuck, Bounced }

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] float speed = 30f;

    public State CurrentState { get; private set; }

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void ResetKnife(Vector3 position)
    {
        transform.SetParent(null);
        transform.SetPositionAndRotation(position, Quaternion.identity);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        col.enabled = true;
        CurrentState = State.Idle;
    }

    public void Throw()
    {
        CurrentState = State.Flying;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.up * speed;
    }

    public void SetAsStuck()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        CurrentState = State.Stuck;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CurrentState != State.Flying) return;

        
        if (other.TryGetComponent(out Knife otherKnife) && otherKnife.CurrentState == State.Stuck)
        {
            Bounce();
            GameEvents.current.KnifeHitKnife();
            return;
        }

        
        if (other.TryGetComponent(out Apple apple))
        {
            apple.Collect();
            return;
        }

       
        if (other.TryGetComponent(out Log log))
        {
            StickTo(log);
            GameEvents.current.KnifeStuck();
        }
    }

    private void StickTo(Log log)
    {
        SetAsStuck();
        Vector3 dir = (transform.position - log.transform.position).normalized;
        transform.position = log.transform.position + dir * log.KnifeStickDistance;
        transform.SetParent(log.transform, true);
    }

    private void Bounce()
    {
        CurrentState = State.Bounced;
        rb.linearVelocity = new Vector2(Random.Range(-4f, 4f), -8f);
        rb.angularVelocity = Random.value > 0.5f ? 900f : -900f;
        rb.gravityScale = 3f;
    }
}
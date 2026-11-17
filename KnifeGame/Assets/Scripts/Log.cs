using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Log : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;

   
    [SerializeField] float knifeStickDistance = 1.3f;
   
    [SerializeField] float appleDistance = 1.1f;
    
    [SerializeField] float acceleration = 400f;

    RotationStep[] steps;
    int stepIndex;
    float stepTimer;
    float currentSpeed;
    bool spinning;

    public float KnifeStickDistance => knifeStickDistance;
    public float AppleDistance => appleDistance;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Setup(LevelData data)
    {
        steps = data.rotation;
        stepIndex = 0;
        stepTimer = 0f;
        currentSpeed = 0f;
        rb.rotation = 0f;
        transform.rotation = Quaternion.identity;
        if (data.logSprite != null) sr.sprite = data.logSprite;
        spinning = true;
    }

    public void Stop() => spinning = false;

    private void FixedUpdate()
    {
        if (!spinning || steps == null || steps.Length == 0) return;

        stepTimer += Time.fixedDeltaTime;
        if (stepTimer >= steps[stepIndex].duration)
        {
            stepTimer = 0f;
            stepIndex = (stepIndex + 1) % steps.Length;
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, steps[stepIndex].speed, acceleration * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation + currentSpeed * Time.fixedDeltaTime);
    }

   
    public void Attach(Transform t, float angleDeg, float distance)
    {
        float worldAngle = transform.eulerAngles.z + angleDeg;
        float rad = worldAngle * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);
        t.SetParent(null);
        t.SetPositionAndRotation(transform.position + dir * distance, Quaternion.Euler(0f, 0f, worldAngle + 90f));
        t.SetParent(transform, true);
    }
}
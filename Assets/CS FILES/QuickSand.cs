using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    public float sinkSpeed = 0.1f;
    public LayerMask sinkableLayers;

    private List<Rigidbody2D> bodiesInside = new List<Rigidbody2D>();

    private Dictionary<Rigidbody2D, float> originalGravity = new Dictionary<Rigidbody2D, float>();
    private Dictionary<Rigidbody2D, RigidbodyConstraints2D> originalConstraints = new Dictionary<Rigidbody2D, RigidbodyConstraints2D>();

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in bodiesInside)
        {
            if (rb == null) continue;

            rb.linearVelocity = new Vector2(0f, -sinkSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((sinkableLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null || bodiesInside.Contains(rb))
            return;

        bodiesInside.Add(rb);

        originalGravity[rb] = rb.gravityScale;
        originalConstraints[rb] = rb.constraints;

        rb.gravityScale = 0f;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((sinkableLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null || !bodiesInside.Contains(rb))
            return;

        bodiesInside.Remove(rb);

        rb.gravityScale = originalGravity[rb];
        originalGravity.Remove(rb);

        rb.constraints = originalConstraints[rb];
        originalConstraints.Remove(rb);
    }
}
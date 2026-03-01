using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    public float baseSinkSpeed = 0.2f;   
    public float massMultiplier = 0.05f; 
    public float maxSinkSpeed = 3f;     

    private List<Rigidbody2D> bodiesInside = new List<Rigidbody2D>();

    void FixedUpdate()
    {
        foreach (var rb in bodiesInside)
        {
            if (rb == null) continue;

            float effectiveMass = GetStackMass(rb);

            float sinkSpeed = baseSinkSpeed + (effectiveMass * massMultiplier);
            sinkSpeed = Mathf.Min(sinkSpeed, maxSinkSpeed);

            Vector2 newPos = rb.position + Vector2.down * sinkSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }


    float GetStackMass(Rigidbody2D baseBody)
    {
        float totalMass = baseBody.mass;

        Bounds baseBounds = baseBody.GetComponent<Collider2D>().bounds;

        foreach (var other in bodiesInside)
        {
            if (other == baseBody || other == null) continue;

            Bounds otherBounds = other.GetComponent<Collider2D>().bounds;

            bool isAbove =
                otherBounds.min.y >= baseBounds.max.y - 0.05f; 

            bool overlapsX =
                otherBounds.max.x > baseBounds.min.x &&
                otherBounds.min.x < baseBounds.max.x;

            if (isAbove && overlapsX)
                totalMass += other.mass;
        }

        return totalMass;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null && !bodiesInside.Contains(rb))
            bodiesInside.Add(rb);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
            bodiesInside.Remove(rb);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerConfigs : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private int jumps = 0;
    public Transform objectHoldPoint;
    public float pickupRadius;
    public LayerMask pickupLayer;
    private GameObject heldObject;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnInteract(InputValue value)
    {
        if(!value.isPressed)
            return;
        if(heldObject != null)
        {
            TryPickup();
        }

    }

    void TryPickup()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius, pickupLayer);
        if(hits.Length == 0)
            return;
        Collider2D closest = hits[0];
        float bestDist = Vector2.Distance(transform.position, closest.transform.position);

        for (int i = 1; i < hits.Length; i++)
        {
            float d = Vector2.Distance(transform.position, hits[i].transform.position);
            if(d < bestDist)
            {
                bestDist = d;
                closest = hits[i];
            }
        }

        PickUp(closest.gameObject);
    }

    void PickUp(GameObject obj)
    {
        heldObject = obj;

        var rb = heldObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        var col = heldObject.GetComponent<Collider2D>();


        heldObject.transform.SetParent(objectHoldPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }

    void Drop()
    {
        
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (jumps <= 1)
        {
            rb2d.linearVelocity = Vector2.up * 10;
            jumps++;
        }
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumps = 0;
        }
    }
}
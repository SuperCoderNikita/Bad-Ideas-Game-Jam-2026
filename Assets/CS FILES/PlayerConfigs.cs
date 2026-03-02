using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigs : MonoBehaviour
{
    public float speed = 5f;
    public float actualSpeed;
    public float jumpHeigt = 3f;
    public Animator anim;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private int jumps = 0;

    public Transform objectHoldPoint;
    public float pickupRadius = 1.2f;
    public LayerMask pickupLayer;
    public GameObject pickupPrompt; 

    private GameObject heldObject;
    private GameObject nearbyObject; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);
        actualSpeed = 0f;
        
    }

    void Update()
    {
        DetectNearbyObject();
        anim.SetFloat("speed", Mathf.Abs(actualSpeed));
    }


    public void OnInteract(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (heldObject != null)
            Drop();
        else
            TryPickup();
    }


    void DetectNearbyObject()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius, pickupLayer);

        if (hits.Length == 0)
        {
            nearbyObject = null;
            if (pickupPrompt != null) pickupPrompt.SetActive(false);
            return;
        }

        Collider2D closest = hits[0];
        float bestDist = Vector2.Distance(transform.position, closest.transform.position);

        for (int i = 1; i < hits.Length; i++)
        {
            float d = Vector2.Distance(transform.position, hits[i].transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                closest = hits[i];
            }
        }

        nearbyObject = closest.gameObject;

        if (pickupPrompt != null && heldObject == null)
            pickupPrompt.SetActive(true);
    }


    void TryPickup()
    {
        if (nearbyObject == null)
            return;

        PickUp(nearbyObject);
    }

    void PickUp(GameObject obj)
    {
        heldObject = obj;

        var rb = heldObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        var col = heldObject.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        heldObject.transform.SetParent(objectHoldPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);
    }


    void Drop()
    {
        if (heldObject == null)
            return;

        heldObject.transform.SetParent(null);

        var rb = heldObject.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;

        var col = heldObject.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        heldObject = null;
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        actualSpeed = 1f;
    }

    public void OnJump(InputValue value)
    {
        if (jumps <= 1)
        {
            rb2d.linearVelocity = Vector2.up * jumpHeigt;
            jumps++;
        }
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Object"))
        {
            jumps = 0;
        }
    }
}
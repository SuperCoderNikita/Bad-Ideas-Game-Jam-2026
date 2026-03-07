using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigs : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;
    public float jumpHeigt = 3f;
    public float throwForce = 8f;

    public Transform objectHoldPoint;
    public float pickupRadius = 1.2f;
    public LayerMask pickupLayer;
    public GameObject pickupPrompt;

    public Animator anim;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;

    private int jumps = 0;

    private PlayerInput playerInput;
    private InputAction sprintAction;
    private bool isSprinting;


    public float actualSpeed;

    private GameObject heldObject;
    private GameObject nearbyObject;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
            sprintAction = playerInput.actions["Sprint"];

        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);

        actualSpeed = 0f;
        isSprinting = false;
    }

    void Update()
    {
        isSprinting = sprintAction != null && sprintAction.IsPressed();

        DetectNearbyObject();

        if (anim != null)
        {
            anim.SetFloat("speed", Mathf.Abs(actualSpeed));
            anim.SetBool("isSprinting", isSprinting);
        }
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.x != 0)
        {
            Vector2 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        float sprint = isSprinting ? sprintMultiplier : 1f;
        actualSpeed = Mathf.Abs(moveInput.x) * sprint;
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (jumps <= 1)
        {
            Vector2 gravityDir = Physics2D.gravity.normalized;
            Vector2 jumpDir = -gravityDir;

            rb2d.linearVelocity = new Vector2(
                rb2d.linearVelocity.x,
                jumpDir.y * jumpHeigt
            );

            jumps++;
        }
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


    void FixedUpdate()
    {
        float currentSpeed = speed * (isSprinting ? sprintMultiplier : 1f);
        rb2d.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb2d.linearVelocity.y);
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
        var col = heldObject.GetComponent<Collider2D>();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (col != null)
            col.enabled = true;

            if (rb != null)
            {
                float facingDir = Mathf.Sign(transform.localScale.x);
                Vector2 throwDirection = new Vector2(facingDir, 0.5f).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
        

        heldObject = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Object"))
            jumps = 0;
    }


}
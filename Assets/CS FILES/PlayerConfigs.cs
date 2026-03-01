using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigs : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private int jumps = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
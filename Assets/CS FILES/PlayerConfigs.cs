using UnityEngine;


public class PlayerConfigs : MonoBehaviour
{

    public float speed;
    private float horizontalInput;
    private Rigidbody2D rb2d;
    bool isOnFloor = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && isOnFloor == true)
        {
            rb2d.linearVelocity = Vector2.up * 10;
            isOnFloor = false;
        }
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(horizontalInput * speed, rb2d.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }
}

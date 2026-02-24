using UnityEngine;


public class PlayerConfigs : MonoBehaviour
{

    public float speed;
    private float horizontalInput;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(horizontalInput * speed, rb2d.linearVelocity.y);
    }
}

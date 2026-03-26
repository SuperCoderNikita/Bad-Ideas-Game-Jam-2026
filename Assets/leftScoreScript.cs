using UnityEngine;

public class leftScoreScript : MonoBehaviour
{
    private float speed = 4;
    private float startX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0) * speed * Time.deltaTime;

        if ((transform.position.x - startX) >= 6.4)
        {
            destroyObject();
        }
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}
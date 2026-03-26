using UnityEngine;

public class downScoreScript : MonoBehaviour
{
    private float speed = 4;
    private float startY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 1) * speed * Time.deltaTime;

        if ((transform.position.y - startY) >= 6.4)
        {
            destroyObject();
        }
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    public List<GameObject> mail;

    public float spawnDelay = 2f;
    public float decreaseAmount = 0.05f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (mail.Count > 0)
        {
            spawn();

            yield return new WaitForSeconds(spawnDelay);

            spawnDelay = Mathf.Max(0.1f, spawnDelay - decreaseAmount);
        }
    }

    void spawn()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(mail[0], spawnPos, Quaternion.identity);

        mail.RemoveAt(0);
    }
}
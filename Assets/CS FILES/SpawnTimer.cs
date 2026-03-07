using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{

    public Transform[] spawnPoints;
    public List<GameObject> mail;
    public float spawnDelay;
    void Start()
    {
        InvokeRepeating("spawn", spawnDelay, spawnDelay);
    }


    void Update()
    {
        
    }


    void spawn()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(mail[0], spawnPoint.position, transform.rotation);

        mail.RemoveAt(0);
    }
}

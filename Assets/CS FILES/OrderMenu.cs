using UnityEngine;
using System.Collections;

public class OrderMenu : MonoBehaviour
{
    public GameObject shapeOne;
    public GameObject shapeTwo;
    public GameObject shapeThree;

    public Transform spawnPoint;
    public float spawnDelay = 0.5f;

    private bool canSpawn = true;

    public void SpawnObjectOne() 
    { 
        TrySpawn(shapeOne); 
    }
    public void SpawnObjectTwo()  
    {
        TrySpawn(shapeTwo); 
    }
    public void SpawnObjectThree()
    {
        TrySpawn(shapeThree);
    }

    void TrySpawn(GameObject shape)
    {
        if (!canSpawn) return;             
        StartCoroutine(SpawnWithCooldown(shape));
    }

    IEnumerator SpawnWithCooldown(GameObject shape)
    {
        canSpawn = false;

        yield return new WaitForSeconds(spawnDelay);

        if (shape != null && spawnPoint != null)
            Instantiate(shape, spawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
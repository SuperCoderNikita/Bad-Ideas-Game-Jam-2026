using UnityEngine;

public class OrderMenu : MonoBehaviour
{
    public GameObject shapeOne;
    public GameObject shapeTwo;
    public GameObject shapeThree;
    public Vector3 spawnPosition;     

    public void spawnObjectOne()
    {
        Instantiate(shapeOne, spawnPosition, Quaternion.identity);
    }

    public void spawnObjectTwo()
    {
        Instantiate(shapeTwo, spawnPosition, Quaternion.identity);
    }

    public void spawnObjectThree()
    {
        Instantiate(shapeThree, spawnPosition, Quaternion.identity);
    }
}

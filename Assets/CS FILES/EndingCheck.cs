using UnityEngine;

public class EndingCheck : MonoBehaviour
{

    public bool isOutsideTheBox = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOutsideTheBox = false;
    }


    public void setEnding()
    {
        isOutsideTheBox = true;
    }
}

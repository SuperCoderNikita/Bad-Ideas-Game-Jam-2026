using UnityEngine;

public class IgnorePlayerGroundCollision : MonoBehaviour
{
void Start()
{
    int playerCollision = LayerMask.NameToLayer("PlayerCollision");
    int quicksandLayer = gameObject.layer;

    Physics2D.IgnoreLayerCollision(playerCollision, quicksandLayer, true);
}

    // Update is called once per frame
    void Update()
    {
        
    }
}

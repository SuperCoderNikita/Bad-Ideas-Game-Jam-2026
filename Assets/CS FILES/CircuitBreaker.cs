using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitBreaker : MonoBehaviour
{
    public GameObject pressEText;
    public GameObject breakerMenu;
    public WorldInverter worldInverter;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            breakerMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            pressEText.SetActive(false);
        }
    }

    public void OnFlipButtonPressed()
    {
        breakerMenu.SetActive(false);

        worldInverter.FlipWorld();
    }
}
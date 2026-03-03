using UnityEngine;

public class CircuitBreaker : MonoBehaviour
{
    public GameObject pressEText;
    public GameObject breakerMenu;
    public WorldInverter worldInverter;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            breakerMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        worldInverter.FlipWorld();
    }
}
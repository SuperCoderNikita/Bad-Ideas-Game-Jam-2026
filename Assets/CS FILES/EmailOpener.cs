using UnityEngine;
using UnityEngine.InputSystem;

public class EmailOpener : MonoBehaviour
{
    public GameObject pressEText;
    public GameObject mail;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            mail.SetActive(true);
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

    public void OnExitButtonPressed()
    {
        mail.SetActive(false);
    }
}
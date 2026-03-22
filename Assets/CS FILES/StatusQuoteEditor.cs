using UnityEngine;
using UnityEngine.InputSystem;

public class StatusQuoteEditor : MonoBehaviour
{
    public GameObject pressEText;
    public GameObject breakerMenu;
    public SpawnTimer spawnTimer;

    private bool playerInRange = false;
    private bool hasScrewdriver = false;
    public StatusQuoteCount quoteCount;

    
    void Update()
    {
        if (playerInRange && hasScrewdriver && Keyboard.current.eKey.wasPressedThisFrame)
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

        if (other.CompareTag("Screwdriver"))
        {
            hasScrewdriver = true;

            Destroy(other.gameObject);
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
    }


    public void editStatusQuote()
    {
        quoteCount.statusQuote = 1f;
        spawnTimer.mail.Clear();
    }
}

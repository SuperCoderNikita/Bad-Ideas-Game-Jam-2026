using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class upArrowScript : MonoBehaviour
{
    Keyboard keyboard;
    private int score;
    public TMP_Text scoreText;
    private bool noteInZone;
    private GameObject currentNote;
    private bool hasScored;

    void Start()
    {
        keyboard = Keyboard.current;
        score = 0;
        hasScored = false;
        noteInZone = false;
    }

    void Update()
    {
        if (noteInZone && keyboard.upArrowKey.wasPressedThisFrame && !hasScored)
        {
            score++;
            currentNote.GetComponent<upScoreScript>().destroyObject();
            noteInZone = false;
            hasScored = true;
        }

        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreArrows"))
        {
            noteInZone = true;
            hasScored = false;
            currentNote = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreArrows"))
        {
            noteInZone = false;
        }
    }
}
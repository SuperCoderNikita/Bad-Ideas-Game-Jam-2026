using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class rightArrowScript : MonoBehaviour
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
        noteInZone = false;
        hasScored = false;
    }

    void Update()
    {
        if (noteInZone && keyboard.rightArrowKey.isPressed && !hasScored)
        {
            score++;
            currentNote.GetComponent<rightScoreScript>().destroyObject();
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
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusQuoteCount : MonoBehaviour
{
    public float statusQuote = 4;
    public TMP_Text display;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        display.text = statusQuote.ToString();
    }
}

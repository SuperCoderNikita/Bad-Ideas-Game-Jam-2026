using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusQuoteCount : MonoBehaviour
{
    public float statusQuote = 4;
    public TMP_Text display;

    // Update is called once per frame
    void Update()
    {
        display.text = statusQuote.ToString();
    }
}

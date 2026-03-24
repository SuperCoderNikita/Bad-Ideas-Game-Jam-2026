using UnityEngine;

public class InboxWinCondition : MonoBehaviour
{

    public StatusQuoteCount quoteCount;
    public EndingCheck check;
    public GameObject winPanel;
    public GameObject failText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winPanel.SetActive(false);
        failText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quoteCount.statusQuote == 0 && check.isOutsideTheBox == true)
        {
            winPanel.SetActive(true);
            failText.SetActive(false);
        } 
        else if(quoteCount.statusQuote == 0 && check.isOutsideTheBox == false)
        {
            winPanel.SetActive(true);
            failText.SetActive(true);
        }
    }
}

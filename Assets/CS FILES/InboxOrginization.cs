using UnityEngine;
using UnityEngine.UI;
public class InboxOrginization : MonoBehaviour
{
     public float bounceForce = 8f;
     public StatusQuoteCount quoteCount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag(gameObject.tag))
        {
            Destroy(other);
            quoteCount.statusQuote -= 1f;
        }
        else
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 bounceDir = (other.transform.position - transform.position).normalized;

                rb.AddForce(bounceDir * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}

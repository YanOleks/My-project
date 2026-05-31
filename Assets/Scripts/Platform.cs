using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                AudioSource playerAudio = collision.gameObject.GetComponent<AudioSource>();
                playerAudio.Play();
            }
        }
    }
}
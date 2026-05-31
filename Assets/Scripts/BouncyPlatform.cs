using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    public float superJumpForce = 20f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null && rb.linearVelocity.y <= 0f)
            {
 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, superJumpForce);

                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    public Sprite emptyTrapSprite;
    public Sprite fullTrapSprite;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = emptyTrapSprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.gameObject.name == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null && rb.linearVelocity.y <= 0f)
            {
                isTriggered = true;
                spriteRenderer.sprite = fullTrapSprite;
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                collision.gameObject.SetActive(false);
                Object.FindAnyObjectByType<GameManager>().TriggerGameOver();
            }
        }
    }
}
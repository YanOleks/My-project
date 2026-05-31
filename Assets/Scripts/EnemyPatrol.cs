using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float patrolDistance = 2f;

    [Header("Combat Settings")]
    public Sprite deadPlayerSprite;

    private float leftEdge;
    private float rightEdge;
    private bool movingRight = true;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        leftEdge = transform.position.x - patrolDistance;
        rightEdge = transform.position.x + patrolDistance;
    }

    void Update()
    {
        if (isTriggered) return;

        if (movingRight)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.right);
            spriteRenderer.flipX = true;

            if (transform.position.x >= rightEdge) movingRight = false;
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);
            spriteRenderer.flipX = false;

            if (transform.position.x <= leftEdge) movingRight = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isTriggered && collision.gameObject.name == "Player")
        {
            isTriggered = true;

            if (audioSource != null) audioSource.Play();

            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.enabled = false;
            }

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.bodyType = RigidbodyType2D.Kinematic;
            }

            SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerSprite != null && deadPlayerSprite != null)
            {
                playerSprite.sprite = deadPlayerSprite;
            }

            Object.FindAnyObjectByType<GameManager>().TriggerGameOver();
        }
    }
}
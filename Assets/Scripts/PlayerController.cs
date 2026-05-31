using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;
    private float moveInput;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                moveInput = -1f;
                spriteRenderer!.flipX = true;
            }
            else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                moveInput = 1f;
                spriteRenderer!.flipX = false;
            }
        }

        if (transform.position.x > 3f)
            transform.position = new Vector3(-3f, transform.position.y, 0);
        else if (transform.position.x < -3f)
            transform.position = new Vector3(3f, transform.position.y, 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
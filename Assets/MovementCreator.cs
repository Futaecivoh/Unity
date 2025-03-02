using UnityEngine;

public class MovementCreator : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 2f;
    [SerializeField] private float SpeedJump = 10f;
    [SerializeField] private float raycastDistance = 1.3f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    public CoinManager cm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Движение
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            rb.linearVelocity = new Vector2(moveInput * SpeedMove, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        // Raycast для проверки земли
        bool wasGrounded = isGrounded; // Запоминаем предыдущее состояние
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
        Debug.Log("Is Grounded: " + isGrounded);

        // Прыжок
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, SpeedJump), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            Debug.Log("Jump started");
        }

        // Ориентация
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Анимации
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Сброс прыжка при приземлении
        if (isGrounded && !wasGrounded) // Только что приземлились
        {
            animator.SetBool("IsJumping", false);
            Debug.Log("Jump ended");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Melon"))
        {
            Destroy(other.gameObject);
             cm.coinCount++;
        }
    }
}
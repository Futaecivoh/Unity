using UnityEngine;

public class MovementCreator : MonoBehaviour
{
    [SerializeField] private float speedMove = 2f;  
    [SerializeField] private float speedJump = 10f;  
    [SerializeField] private float rayDistance = 1.3f;  
    public LayerMask groundLayer;  
    private AudioSource soundJump;  
    private bool isGrounded = true;  
    private Rigidbody2D rb; 
    private SpriteRenderer spriteRenderer; 

    public Animator animator;  
    public CoinManager cm;  
    private bool playing = true;  
    private bool lookRight; 

    private float yVelocity = 0f;

    public bool LookRight { get => lookRight; set => lookRight = value; }

    public bool IsFacingLeft => spriteRenderer.flipX;  
    public SpriteRenderer SpriteRenderer => spriteRenderer;  

    public bool Playing { get => playing; set => playing = value; } 

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        soundJump = GetComponent<AudioSource>();  
        lookRight = true; 
    }

     void Update()

    {
        if (playing)
        {
            float moveInput = Input.GetAxis("Horizontal");  

            if (moveInput != 0)
            {
                rb.linearVelocity = new Vector2(moveInput * speedMove, rb.linearVelocity.y); 

                if (moveInput > 0 && spriteRenderer.flipX)
                    Flip();
                else if (moveInput < 0 && !spriteRenderer.flipX)
                    Flip();
            }
            else
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }

            bool wasGrounded = isGrounded;
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))  
            {
                Jump();
            }

            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x)); 
            animator.SetFloat("yVelocity",rb.linearVelocity.y);

            if (isGrounded && !wasGrounded)
                animator.SetBool("IsJumping", false); 
                yVelocity = -1;

            if (isGrounded && !Input.GetKey(KeyCode.Space))
                animator.SetBool("IsJumping", false); 
                yVelocity = 0; 
        }


        
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;  
        lookRight = !lookRight;  
    }

    private bool IsGrounded()
    {
        
        return Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer); 
    }


    private void Jump()
    {
        rb.AddForce(new Vector2(0, speedJump), ForceMode2D.Impulse);  
        animator.SetBool("IsJumping", true);  
        soundJump.Play(); 
    }
}

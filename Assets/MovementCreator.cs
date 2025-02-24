 using UnityEngine;

public class MovementCreator : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 2f;
    [SerializeField] private float SpeedJump = 10f;
    
    private bool jump = true;
    private Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
  
    // Update is called once per frame
    void Update()
    {
    //transform.Translate(new Vector2(Input.GetAxis("Horizontal"), 0) * SpeedMove * Time.deltaTime);
    if(Input.GetAxis("Horizontal")!=0)
    {
        rb.linearVelocityX = Input.GetAxis("Horizontal") * SpeedMove;
    }
    //rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * SpeedMove, 0));

    if (jump && Input.GetKey(KeyCode.Space))
    {
        rb.AddForce(new Vector2(0, SpeedJump), ForceMode2D.Impulse);
        jump = false; 
        animator.SetBool("IsJumping", true);
    }
    float move = Input.GetAxis("Horizontal");

    if (move > 0) {
        spriteRenderer.flipX = false;  // Смотрит вправо
    } else if (move < 0) {
        spriteRenderer.flipX = true;   // Смотрит влево
    }
    animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!jump)
        {
        jump = true;
        animator.SetBool("IsJumping", false);
        }
    }
}
  

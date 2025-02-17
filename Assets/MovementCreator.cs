using UnityEngine;

public class MovementCreator : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 2f;
    [SerializeField] private float SpeedJump = 10f;
    
    private bool jump = true;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
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
    }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!jump)
        {
        jump = true;
        }
    }
}
  

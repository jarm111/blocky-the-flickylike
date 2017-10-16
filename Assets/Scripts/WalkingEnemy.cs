using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{

    public float MoveForce = 50f;
    public float MaxSpeed = 3f;
    public float JumpForce = 400f;
    public bool StartFacingRight = true;

    private float maxHorizontalInputValue = 1f;
    private float horizontalInput;
    private bool isJump;
    private bool isFacingRight;

    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFacingRight = StartFacingRight;
        horizontalInput = (isFacingRight) ? maxHorizontalInputValue : -maxHorizontalInputValue;
    }

    private void FixedUpdate()
    {
        Move();

        if (isJump)
            Jump();

        if (rb2d.velocity.y != 0)
            anim.SetBool("inAir", true);
        else
            anim.SetBool("inAir", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTriggerJump"))
            isJump = true;
        if (collision.gameObject.CompareTag("EnemyTriggerFlipDirection"))
            FlipDirection();
    }

    private void Move()
    {
        if (horizontalInput * rb2d.velocity.x < MaxSpeed)
            rb2d.AddForce(Vector2.right * horizontalInput * MoveForce);
        if (Mathf.Abs(rb2d.velocity.x) > MaxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * MaxSpeed, rb2d.velocity.y);
    }

    private void Jump()
    {
        rb2d.AddForce(Vector2.up * JumpForce);
        isJump = false;
    }

    private void FlipDirection()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        horizontalInput = (isFacingRight) ? maxHorizontalInputValue : -maxHorizontalInputValue;
    }
}

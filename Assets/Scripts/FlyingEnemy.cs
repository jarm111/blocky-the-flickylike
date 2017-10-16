using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float XSpeed = 3f;
    public float YSpeed = 3f;
    public float DiveMultiplier = 2f;
    public float MinXLimit = -6f;
    public float MaxXLimit = 7f;
    public float MinYLimit = 2f;
    public float MaxYLimit = 10f;
    public bool StartFacingRight = true;

    private float currentXSpeed;
    private float currentYSpeed;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentXSpeed = (StartFacingRight) ? XSpeed : -XSpeed;
        currentYSpeed = YSpeed;
    }

    private void Update()
    {
        SetSpeed();

        SetFacing();

        SetAnimation();
    }

    private void FixedUpdate()
    {
        //if (maxXLimit <= transform.position.x || transform.position.x <= minXLimit)
        //    FlipHorizontalDirection();

        //if (maxYLimit <= transform.position.y || transform.position.y <= minYLimit)
        //    FlipVerticalDirection();

        //if (currentXSpeed < 0)
        //    spriteRenderer.flipX = true;
        //else
        //    spriteRenderer.flipX = false;

        //SetSpeed();

        //SetFacing();

        rb2d.velocity = new Vector2(currentXSpeed, currentYSpeed);
    }

    //private void FlipHorizontalDirection()
    //{
    //    //horizontalSpeed *= -1f;
    //    XSpeed = -XSpeed;
    //    spriteRenderer.flipX = !spriteRenderer.flipX;
    //}
    //private void FlipVerticalDirection()
    //{
    //    //verticalSpeed *= -1f;
    //    YSpeed = -YSpeed;
    //    if (YSpeed < 0)
    //        YSpeed *= diveMultiplier;
    //    else
    //        YSpeed /= diveMultiplier;
    //}

    private void SetSpeed()
    {
        if (MaxXLimit < transform.position.x)
            currentXSpeed = -XSpeed;

        if (transform.position.x < MinXLimit)
            currentXSpeed = XSpeed;

        if (MaxYLimit < transform.position.y)
            currentYSpeed = -YSpeed * DiveMultiplier;

        if (transform.position.y < MinYLimit)
            currentYSpeed = YSpeed;
    }

    private void SetFacing()
    {
        spriteRenderer.flipX = (currentXSpeed < 0) ? true : false;
    }

    private void SetAnimation()
    {
        if (currentYSpeed < 0)
            anim.SetBool("diving", true);
        else
            anim.SetBool("diving", false);
    }
}

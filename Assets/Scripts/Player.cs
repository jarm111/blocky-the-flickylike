using UnityEngine;

public class Player : MonoBehaviour
{

    private static Player instance;
    public static Player Instance
    {
        get { return instance; }
    }

    private bool isAlive = true;
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    public float HitPoints = 1f;
    public float MoveForce = 50f;
    public float MaxSpeed = 6f;
    public float JumpForce = 400f;
    public float JumpCutModifier = 20f;

    //public GameObject MainCamera;
    public GameObject Follower;
    public GameObject DeadPlayer;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    private float horizontalInput = 0f;
    private bool isJump = false;
    private bool jumpIsOff = false;
    private bool isGrounded = true;
    //private float groundCheckRadius = 0.5f;
    private Vector2 groundCheckSize = new Vector2(0.9f, 0.1f);
    private bool facingRight = true;

    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //MainCameraIsAttached(true);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        isGrounded = CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
            isJump = true;

        if (Input.GetButtonUp("Jump"))
            jumpIsOff = true;

        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("velocityX", Mathf.Abs(rb2d.velocity.x));

        if (HitPoints <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            Move();
            FacingDirection();
        }

        if (isJump)
            Jump();

        if (jumpIsOff)
            CutJumpHeight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageTrigger"))
        {
            ApplyDamage(collision.gameObject.GetComponent<DamageTrigger>().Damage);
            Debug.Log("HP: " + HitPoints);
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            SpawnFollower();
            LevelStateManager.Instance.SubtractCollectiblesRemaining();
            AudioManager.Instance.PlaySfx(0);
            Debug.Log("Follower spawned!");
        }
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
        AudioManager.Instance.PlaySfxVaried(3);
    }

    private void CutJumpHeight()
    {
        if (rb2d.velocity.y > 0)
            rb2d.AddForce(Vector2.down * rb2d.velocity.y * JumpCutModifier);
        jumpIsOff = false;
    }

    private bool CheckIfGrounded()
    {
        //return Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, GroundLayer);
        return Physics2D.OverlapCapsule(GroundCheck.position, groundCheckSize, CapsuleDirection2D.Horizontal, 90, GroundLayer);
    }

    private void FacingDirection()
    {
        //if (horizontalInput > 0.0f && !facingRight)
        //{
        //    facingRight = true;
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //    global::Follower.FlipFacing();
        //}
        //else if (horizontalInput < 0.0f && facingRight)
        //{
        //    facingRight = false;
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //    global::Follower.FlipFacing();
        //}
        if (horizontalInput > 0.0f && !facingRight)
            facingRight = true;
        else if (horizontalInput < 0.0f && facingRight)
            facingRight = false;
        else
            return;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        global::Follower.FlipFacing();
    }

    private void ApplyDamage(float damage)
    {
        HitPoints -= damage;
    }

    private void Die()
    {
        isAlive = false;
        //MainCameraIsAttached(false);
        SpawnDeadPlayer();
        AudioManager.Instance.PlaySfx(1);
        gameObject.SetActive(false);
    }

    //private void MainCameraIsAttached(bool state)
    //{
    //    if (state)
    //        MainCamera.transform.parent = gameObject.transform;
    //    else
    //        MainCamera.transform.parent = null;
    //}

    private void SpawnFollower()
    {
        Instantiate(Follower, gameObject.transform.position, Quaternion.identity);
    }

    private void SpawnDeadPlayer()
    {
        Instantiate(DeadPlayer, gameObject.transform.position, Quaternion.identity);
    }
}

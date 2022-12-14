using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float normalMoveSpeed = 2f;

    private float moveSpeed = 2f;
    private float boostSpeed = 0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;
    private float particleTimer;
    private SpriteRenderer playerSpriteRenderer;

    public float BoostSpeed { get => boostSpeed; set => boostSpeed = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particleTimer = 0;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveSpeed = normalMoveSpeed - ((float)Backpack.ItemsCount / 10) + BoostSpeed;
        rb.velocity = new Vector2(0, 0);

        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            MoveCharacter();
            animator.SetBool("isMoving", true);

            if (movement.x > 0)
            {
                playerSpriteRenderer.flipX = false;
            }
            if (movement.x < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
        }
        else
        {

            animator.SetBool("isMoving", false);

        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void SpawnParticles()
    {
        if (particleTimer < 0.3f)
        {
            particleTimer += Time.deltaTime;
        }
        else
        {
            Instantiate(walkingParticles, feetPos.transform.position, transform.rotation, GameObject.Find("Particles").transform);
            particleTimer = 0f;
        }
    }

    void MoveCharacter()
    {
        if(movement.x != 0 || movement.y != 0)
        {
            SpawnParticles();
        }
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

    }
}

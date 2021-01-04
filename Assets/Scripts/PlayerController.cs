using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isGrounded;
    private Vector3 initialPosition;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float runSpeed = 2f;

    [SerializeField]
    private float jumpSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // handle run
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            sr.flipX = false;
            if(isGrounded)
                animator.Play("PlayerRun");
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            sr.flipX = true;
            if(isGrounded)
                animator.Play("PlayerRun");
        }
        // handle idle state
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if(isGrounded)
                animator.Play("PlayerIdle");
        }

        // handle jump
        if(Input.GetKey("space") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            animator.Play("PlayerJump");
        }

        // handle fall
        if(rb.velocity.y < 0.1 && !isGrounded)
        {
            animator.Play("PlayerFall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.name == "BoundsBottom")
        {
            transform.position = initialPosition;
        }
    }
}

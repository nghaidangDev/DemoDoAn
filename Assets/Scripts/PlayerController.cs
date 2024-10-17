using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float groundCheckRadious;
    [SerializeField] private int amountOfJumps;

    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovemnet();
        CheckSurroundings();
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
            amountOfJumpsLeft = amountOfJumps;

        if (amountOfJumpsLeft <= 0)
            canJump = false;
        else
            canJump = true;
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, whatIsGrounded);
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
            Flip();
        else if (!isFacingRight && movementInputDirection > 0)
            Flip();

        if (rb.velocity.x != 0)
            isWalking = true;
        else 
            isWalking = false;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void ApplyMovemnet()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }
    
    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadious);
    }
}

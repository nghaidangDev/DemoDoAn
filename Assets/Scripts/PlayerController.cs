using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 16.0f;

    private bool isFacingRight = true;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovemnet();
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
            Flip();
        else if (!isFacingRight && movementInputDirection > 0)
            Flip();
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Space))
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermove : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public Animator animator;
    // 1. Add this to control the sprite
    public SpriteRenderer spriteRenderer; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Automatically find the sprite renderer on this object
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        bool isMoving = moveInput != Vector2.zero;
        animator.SetBool("iswalk", isMoving);

        // --- NEW FLIP LOGIC ---
        // If moving RIGHT (positive X), face right
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        // If moving LEFT (negative X), face left
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // ----------------------
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
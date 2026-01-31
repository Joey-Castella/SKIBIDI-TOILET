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
    public SpriteRenderer spriteRenderer;

    // --- NEW AUDIO VARIABLES ---
    public AudioSource audioSource;   // Drag AudioSource component here
    public AudioClip footstepSound;   // Drag your step sound here
    public float stepRate = 0.5f;     // Time between steps (lower = faster)
    private float nextStepTime = 0f;  // Internal timer
    // ---------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Automatically find audio source if it's on the same object
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        bool isMoving = moveInput != Vector2.zero;
        animator.SetBool("iswalk", isMoving);

        // --- FOOTSTEP LOGIC ---
        // 1. Are we moving?
        // 2. Is it time for the next step?
        if (isMoving && Time.time >= nextStepTime)
        {
            PlayFootstep();
            // Reset the timer
            nextStepTime = Time.time + stepRate;
        }
        // ----------------------

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void PlayFootstep()
    {
        if (audioSource != null && footstepSound != null)
        {
            // Optional: Change pitch slightly so it doesn't sound robotic
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(footstepSound);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
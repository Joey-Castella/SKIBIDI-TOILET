using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Needed if you want a Stamina Bar later

public class playermove : MonoBehaviour
{
    // --- MOVEMENT SETTINGS ---
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    private float currentSpeed; // This changes based on what you are doing
    
    // --- STAMINA SETTINGS ---
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrain = 20f; // How fast it drains (per second)
    public float staminaRegen = 10f; // How fast it recovers
    public Slider staminaBar;        // (Optional) Drag a UI Slider here to see it

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isSprintPressed; // Tracks if Shift is held

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // --- AUDIO VARIABLES ---
    public AudioSource audioSource;   
    public AudioClip footstepSound;   
    public float walkStepRate = 0.5f;   // Normal step speed
    public float sprintStepRate = 0.3f; // Faster step speed
    private float nextStepTime = 0f;  
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        // Start with full stamina
        currentStamina = maxStamina;
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // 1. CALCULATE STAMINA & SPEED
        // You are sprinting if: Moving + Shift Held + Have Stamina
        bool isMoving = moveInput != Vector2.zero;
        bool isSprinting = isMoving && isSprintPressed && currentStamina > 0;

        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
            // Drain Stamina
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            currentSpeed = walkSpeed;
            // Regen Stamina (only if we are not full)
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }

        // Clamp Stamina (Keep it between 0 and 100)
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Update UI Bar (if you have one)
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }

        // 2. APPLY MOVEMENT
        rb.linearVelocity = moveInput * currentSpeed;

        // 3. ANIMATION
        animator.SetBool("iswalk", isMoving);
        // Optional: You could add animator.SetBool("issprint", isSprinting);

        // 4. FOOTSTEPS
        // Switch step rate based on speed
        float currentStepRate = isSprinting ? sprintStepRate : walkStepRate;

        if (isMoving && Time.time >= nextStepTime)
        {
            PlayFootstep();
            nextStepTime = Time.time + currentStepRate;
        }

        // 5. FLIP SPRITE
        if (moveInput.x > 0) spriteRenderer.flipX = false;
        else if (moveInput.x < 0) spriteRenderer.flipX = true;
    }

    void PlayFootstep()
    {
        if (audioSource != null && footstepSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(footstepSound);
        }
    }

    // --- INPUT FUNCTIONS ---

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // YOU MUST LINK THIS IN UNITY INPUT SYSTEM
    public void Sprint(InputAction.CallbackContext context)
    {
        // "performed" means button is down, "canceled" means button released
        if (context.performed)
        {
            isSprintPressed = true;
        }
        if (context.canceled)
        {
            isSprintPressed = false;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private bool lastInputWasHorizontal = false;
    private Vector2 lastDirection = Vector2.down; // Default idle direction (e.g., facing down)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get Animator component
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.performed)
        {
            bool isHorizontalInput = Mathf.Abs(input.x) > Mathf.Abs(input.y);

            if (isHorizontalInput != lastInputWasHorizontal)
            {
                movement = Vector2.zero; // Reset movement when switching axes
            }

            if (isHorizontalInput)
            {
                movement.x = input.x;
                movement.y = 0f;
            }
            else
            {
                movement.y = input.y;
                movement.x = 0f;
            }

            lastInputWasHorizontal = isHorizontalInput;
            lastDirection = movement.normalized; // Store last direction
        }
        else if (context.canceled)
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        bool isWalking = movement.sqrMagnitude > 0.01f;

        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
        else
        {
            // Apply last direction when idle
            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
        }
    }
}

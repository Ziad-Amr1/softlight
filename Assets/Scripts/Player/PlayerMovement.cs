using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private PlayerControls input;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection = Vector2.down;

    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    private void Update()
    {
        direction = input.Gameplay.Move.ReadValue<Vector2>().normalized;
        bool isMoving = direction != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            lastDirection = direction;
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.SetFloat("idleX", lastDirection.x);
        animator.SetFloat("idleY", lastDirection.y);
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }
}
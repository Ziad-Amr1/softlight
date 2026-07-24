using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private PlayerControls input;

    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();
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
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }
}
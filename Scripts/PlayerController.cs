using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public PlayerInputActions InputActions;
    
    [Header("Movement Settings")] 
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private Rigidbody2D rb;
    private float movementMultiplier = 1f;
    private Vector2 moveDirection;
    private Vector2 rotationDirection;
    
    [Header("Collision Settings")]
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask hitLayers;
    private float targetDistance;
    private bool obstacleInPath = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        InputActions.Player.Enable();
        
        InputActions.Player.Move.performed += context => moveDirection = context.ReadValue<Vector2>();
        InputActions.Player.Move.canceled += context => moveDirection = Vector2.zero;
        
        InputActions.Player.Look.performed += context => rotationDirection = context.ReadValue<Vector2>();
        InputActions.Player.Look.canceled += context => rotationDirection = Vector2.zero;
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
    }
    
    void Update()
    {
        if (GameStateManager.Instance.GetGameState() == GameState.Paused)
        {
            return;
        }
        
        MovementHandler();
        RotationHandler();
    }

    public void MovementHandler()
    {
        if (!obstacleInPath)
        {
            Vector2 movement = transform.up * moveDirection.y;
            rb.linearVelocity = movement * movementSpeed;
        }
    }

    public void RotationHandler()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = (mouseWorldPosition - transform.position);
        direction.z = 0; 

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.up = direction;
        }
    }

}

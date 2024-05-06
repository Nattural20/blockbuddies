using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody hand1, head;
    public Collider groundCheck;

    public Transform cameraTransform;


    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;

    public bool isGrounded = false;

    bool isHoldingGrab = false;
    public GameObject currentBlock;

    public float rotationSpeed = 5f;

    public float acceleration = 5f;
    public float maxSpeed = 5f;
    public float deceleration = 5f;
    public float pivotSpeed = 5;

    public float jumpForce = 10f;
    public float floatDuration = 0.5f;
    public float descentForce = 20f;
    public bool isJumping, isFallingAfterJump = false;
    private float jumpTimer;

    public float headThrust = 60;
    public float armThrust = 60;

    private Vector3 velocity;
    private Vector2 lastMove;

    // Moving platform velocity transferral
    private Rigidbody movingPlatform;
    private MovingPlatform platformMove;
    bool onPlatform;

    private Vector3 lastMovementDirection = Vector3.forward;

    void Awake()
    {
        controls = new PlayerControls();

        //.started .performed .cancelled

        controls.Gameplay.Grab.performed += ctx => isHoldingGrab = true;
        controls.Gameplay.Grab.canceled += ctx => isHoldingGrab = false;

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => StartJump();
    }

    private void Update()
    {
        if (isHoldingGrab)
        {
            Grab();
        }

        if (isJumping)
        {
            ManageJump();
        }



        Vector2 r = new Vector2(rotate.x, rotate.y) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyHeadThrust();
        RotatePlayer();

        if (isFallingAfterJump && !isGrounded)
        {
            rb.AddForce(Vector3.down * descentForce, ForceMode.Impulse);
        }

    }

    void RotatePlayer()
    {
        if (move.x != 0 || move.y != 0)
        {
          lastMovementDirection = new Vector3(-move.y, 0f, move.x);
        }
        Quaternion targetRotation = Quaternion.LookRotation(lastMovementDirection);
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ApplyMovement()
    {
        // Convert move from 2D to 3D space
        Vector3 moveInput = new Vector3(move.x, 0, move.y);

        // Transform moveInput to be relative to the camera's rotation
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Keep the movement strictly horizontal
        Vector3 right = cameraTransform.right;
        right.y = 0; // Keep the movement strictly horizontal

        // Create the movement vector relative to the camera's orientation
        Vector3 movementDirection = (forward * moveInput.z + right * moveInput.x).normalized;

        Vector3 accelerationVector = movementDirection * acceleration;

        // Check if direction has significantly changed
        if (Vector2.Dot(lastMove.normalized, new Vector2(movementDirection.x, movementDirection.z).normalized) < 0.8f && move != Vector2.zero)
        {
            // Pivot faster
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * pivotSpeed * Time.fixedDeltaTime);
        }
        else if (move == Vector2.zero)
        {
            // Normal deceleration if let go
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Normal acceleration
            velocity += accelerationVector * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        lastMove = new Vector2(movementDirection.x, movementDirection.z);
    
    }

    void Grab()
    {
       // hand1.AddForce(lastMovementDirection * armThrust);
        hand1.AddForceAtPosition(new Vector3(0, armThrust, 0), new Vector3 (0,-5,0));
        //hand2.AddForce(transform.right * armThrust);
    }

    void StartJump()
    {
        isGrounded = false;

        Debug.Log("OK");

        if (!isJumping) 
        {
            Debug.Log("OKSSSS");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            jumpTimer = floatDuration;
        }
    }

    void ManageJump()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        else if (jumpTimer <= 0 && isJumping)
        {
            isJumping = false;
            isFallingAfterJump = true;
        }
    }

    void ApplyHeadThrust()
    {
        head.AddForce(transform.up * headThrust);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();   
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            onPlatform = true;
            movingPlatform = collision.gameObject.GetComponent<Rigidbody>();
            platformMove = collision.gameObject.GetComponent<MovingPlatform>();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            onPlatform = false;
            movingPlatform = null;
            platformMove = null;
        }
    }
}

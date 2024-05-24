using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public AudioManager aM;

    public Rigidbody rb;
    public Rigidbody hand1, hand2, head, cubert;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded = false;


    public Transform cameraTransform;


    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;


    public bool isHoldingGrab = false;
    public GameObject currentBlock;

    public float rotationSpeed = 5f;

    //movement
    public float acceleration = 5f;
    public float maxSpeed = 5f;
    public float deceleration = 5f;
    public float pivotSpeed = 5;

    //jump
    public float jumpForce = 10f;
    public bool isJumping, isFallingAfterJump = false;
    private float jumpTimer;

    bool isHoldingJump;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;


    public float headThrust = 60;
    public float armThrust = 60;

    private Vector3 velocity;
    private Vector2 lastMove;

    // Moving platform velocity transferral
    private Rigidbody movingPlatform;
    private MovingPlatform platformMove;
    bool onPlatform;

    private Vector3 currentMovementDirection;
    private Vector3 lastMovementDirection;
    private Vector3 currentFacingDirection;

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
        controls.Gameplay.Jump.performed += ctx => isHoldingJump = true;
        controls.Gameplay.Jump.canceled += ctx => isHoldingJump = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (isHoldingGrab)
        {
            Grab();
        }




        Vector2 r = new Vector2(rotate.x, rotate.y) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyHeadThrust();
        RotatePlayer();

        currentFacingDirection = rb.transform.right;



        //making the jump better
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isHoldingJump)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void RotatePlayer()
    {
        //for some reason the currentMoveDir needs to be flipped
        Vector3 newDir = new Vector3(-currentMovementDirection.z, 0, currentMovementDirection.x);

        if (newDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(newDir);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ApplyMovement()
    {
        Vector3 moveInput = new Vector3(move.x, 0, move.y);

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        Vector3 right = cameraTransform.right;
        right.y = 0; 

        currentMovementDirection = (forward * moveInput.z + right * moveInput.x).normalized;

        Vector3 accelerationVector = currentMovementDirection * acceleration;

        if (Vector2.Dot(lastMove.normalized, new Vector2(currentMovementDirection.x, currentMovementDirection.z).normalized) < 0.8f && move != Vector2.zero)
        {
            //pivot faster
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * pivotSpeed * Time.fixedDeltaTime);
            lastMovementDirection = new Vector3(currentMovementDirection.x,0, currentMovementDirection.z);
        }
        else if (move == Vector2.zero)
        {
            //normal deceleration if let go
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            //normal acceleration
            velocity += accelerationVector * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        lastMove = new Vector2(currentMovementDirection.x, currentMovementDirection.z);

    }

    void Grab()
    {

        Vector3 newDir = new Vector3(lastMove.x, 0, lastMovementDirection.z);
        Debug.Log(lastMovementDirection);
        // hand1.AddForce(lastMovementDirection * armThrust);
        hand1.AddForce(currentFacingDirection * armThrust);
        hand2.AddForce(currentFacingDirection * armThrust);
    }

    void StartJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            //aM.Play("HopperJump");
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

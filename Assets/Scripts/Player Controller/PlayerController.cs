using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public AudioManager aM;

    public Rigidbody rb;
    public Rigidbody hand1, hand2, head, cubert;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float groundRayDistance = 1;
    public float groundMaxAngle = 45;
    public LayerMask groundMask;

    public bool isGrounded = false;


    public Transform cameraTransform;


    PlayerControls controls;
    PauseMenuController pause;
    public GameObject pauseObject;

    Vector2 move;
    Vector2 rotate;


    public bool isHoldingGrab = false;
    bool playedGrabSound = false;
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
    //private Rigidbody movingPlatform;
    //private MovingPlatform platformMove;
    //bool onPlatform;

    public Vector3 currentMovementDirection;
    private Vector3 lastMovementDirection;
    private Vector3 currentFacingDirection;

    // Action tracking data


    void Awake()
    {
        controls = new PlayerControls();
        pause = pauseObject.GetComponent<PauseMenuController>();

        //.started .performed .cancelled

        controls.Gameplay.Grab.performed += ctx => isHoldingGrab = true;
        controls.Gameplay.Grab.canceled += ctx => isHoldingGrab = false;

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => StartJump();



        /*
        controls.Gameplay.Jump.performed += ctx => isHoldingJump = true;
        controls.Gameplay.Jump.canceled += ctx => isHoldingJump = false;
        */


        controls.Gameplay.Menu.performed += ctx => pause.PauseMenu();

        
        



    }

    private void Update()
    {






        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = CheckGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (isHoldingGrab)
        {
            if (!playedGrabSound)
            {
                FindAnyObjectByType<AudioManager>().Play("BlockPickup"); //Sound effect script- this line plays a sound from the AudioManager.
                playedGrabSound = true;
            }


            Grab();
        }

        if (!isHoldingGrab)
        {
            playedGrabSound = false;
        }




        Vector2 r = new Vector2(rotate.x, rotate.y) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyHeadThrust();
        RotatePlayer();

        currentFacingDirection = rb.transform.right;



        /*

        //making the jump better
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isHoldingJump)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        */
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
            FindAnyObjectByType<AudioManager>().Play("HopperJump"); //Sound effect script- this line plays a sound from the AudioManager.
            //aM.Play("HopperJump");
        }

        
    }




    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("BOISJDFBJSDBFJDSB");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    bool CheckGrounded()
    {
        RaycastHit hit;

        Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Physics.SphereCast(groundCheck.position, groundDistance, Vector3.down, out hit, groundRayDistance))
        {
            float groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            //Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
            //
            //Vector3 groundSlopeDir = Vector3.Cross(temp, hit.normal);

            if (groundSlopeAngle < groundMaxAngle)
            {
                Debug.Log("Player is grounded on " + hit.collider.name);
                return true;
            }
            else
            {
                Debug.Log("Player is not grounded (sphere check)");
                return false;
            }
        }
        else
        {
            Debug.Log("Player is not grounded (no sphere check)");
            return false;

        }
    }
}

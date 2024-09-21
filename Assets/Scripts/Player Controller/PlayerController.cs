using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public bool isGrounded = false;




    //jump buffer stuff
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    public  bool jumpQueued;

    public float jumpCooldown = .5f;


    public AudioManager aM;

    public Rigidbody rb;
    public Rigidbody hand1, hand2, head, cubert;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float groundRayDistance = 1;
    public float groundMaxAngle = 45;
    public float spockGroundMaxAngle = 70;
    public LayerMask groundMask;

    public float coyoteTime = 0.2f;
    private bool canCoyotetime = true;
    private float timeSinceLastGrounded;

    //timer to check if player is not falling
    private float verticalVelocityCheck;
    public float timeBetweenVerticalVelocityCheck = 0.5f;
    private bool verticalVelocityZero = false;


    public Transform cameraTransform;


    PlayerControls controls;
    PauseMenuController pause;
    public GameObject pauseObject;
    private bool resetLag = true;

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

    public GhostSpocksController gS;
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






        //SPOCK STUFF
        controls.Gameplay.GhostHeightIncrease.performed += ctx =>  gS.UnIncreaseGhostHeight();
        controls.Gameplay.GhostHeightDecrease.performed += ctx =>  gS.IncreaseGhostHeight();





        controls.Gameplay.GhostDistanceIncrease.performed += ctx => gS.increasingDistance = true;
        controls.Gameplay.GhostDistanceIncrease.canceled += ctx => gS.increasingDistance = false;

        controls.Gameplay.GhostDistanceDecrease.performed += ctx => gS.decreasingDistance = true;
        controls.Gameplay.GhostDistanceDecrease.canceled += ctx => gS.decreasingDistance = false;

        //controls.Gameplay.GhostDistanceDecrease.performed += ctx => gS.UnIncreaseGhostHeight();






    }

    private void Update()
    {
        isGrounded = CheckGrounded();

        if (pause.isPaused == true && Input.GetKey(KeyCode.Joystick1Button6) && Input.GetKey(KeyCode.Joystick1Button7))
        {
            ResetScene();
        }

        //Ground check 
        ExtraGroundCheck();

        //if grounded and moving downward, reset vertical velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Grab 
        if (isHoldingGrab)
        {
            if (!playedGrabSound)
            {
                FindAnyObjectByType<AudioManager>().Play("BlockPickup");
                playedGrabSound = true;
            }
            Grab();
        }
        else
        {
            playedGrabSound = false;
        }

        //process rotation
        Vector2 r = new Vector2(rotate.x, rotate.y) * Time.deltaTime;

        //Jump buffer logic
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
            if (isGrounded)
            {
                rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
                PerformJump(); 
                jumpBufferCounter = 0; 
                Debug.Log("Jump performed from buffer");
            }
        }

        if (jumpCooldown > -.1f)
        {
            jumpCooldown -= Time.deltaTime;
        }

    }



    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyHeadThrust();
        RotatePlayer();

        currentFacingDirection = rb.transform.right;

        ApplyForwardRotation();


        ApplyCustomGravity();
    }




    void ApplyForwardRotation()
    {


        float currentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        float targetZRotation = Mathf.Lerp(0, -60, currentSpeed / maxSpeed);

        Quaternion currentRotation = rb.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, targetZRotation);

        rb.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

        //Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, 0);
        
    }

    void ApplyCustomGravity()
    {
        if (rb.velocity.y < 0) // Falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !isHoldingJump) // Jump released early
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
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
            PerformJump();
            Debug.Log("Immediate jump performed");
        }
        else
        {
            jumpBufferCounter = jumpBufferTime; // Buffer the jump if not grounded
            Debug.Log("Jump buffered");
        }
    }

    void PerformJump()
    {
        if (jumpCooldown < 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StopCoroutine(CoyoteCooldown());
            StartCoroutine(CoyoteCooldown());
            isGrounded = false;
            jumpQueued = false;
            Debug.Log("JUMP");

            // Reset jump cooldown
            jumpCooldown = 0.5f; // Adjust as needed
        }
    }


    IEnumerator CoyoteCooldown()
    {
        canCoyotetime = false;
        yield return new WaitForSeconds(coyoteTime);
        canCoyotetime = true;
    }


    public void ResetScene()
    {
        FindObjectOfType<ResetManager>().ResetScene();
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

    private bool CheckGrounded()
    {


        if (Physics.SphereCast(groundCheck.position, groundDistance, Vector3.down, out RaycastHit hit, groundRayDistance)) //Casts sphere downwards along a ray
        {
            float groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up); // Gets the angle of the hit

            //Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);    
            //
            //Vector3 groundSlopeDir = Vector3.Cross(temp, hit.normal);   // Code stolen from the tutorial. Idk what theses add, just more angle check I think

            if (hit.collider.gameObject.layer == groundMask) //If it is a spock, check a steeper angle
            {
                if (groundSlopeAngle < spockGroundMaxAngle) // If the spock is less than the angle, grounded is true
                {
                    return GroundCheckReturn(true, true);
                    //timeSinceLastGrounded = 0f;
                    //return true;
                }
                else
                {
                    if (canCoyotetime && timeSinceLastGrounded < coyoteTime) // If player has been ungrounded for only a little bit, let them jump
                        return GroundCheckReturn(true, false);
                    else
                        return GroundCheckReturn(false, false);
                }
            }
            else if (groundSlopeAngle < groundMaxAngle && !hit.collider.isTrigger) // If hit is not a spock or a trigger, checks if the hit face is less then ground angle
            {
                Debug.Log("Ray hit. Player is grounded on " + hit.collider.name + " at " + groundSlopeAngle + " degrees");
                return GroundCheckReturn(true, true);
                //timeSinceLastGrounded = 0f;
                //return true;
            }
            else
            {
                Debug.Log("Ray hit. Player is not grounded" + " at " + groundSlopeAngle + " degrees");
                if (canCoyotetime && timeSinceLastGrounded < coyoteTime)
                    return GroundCheckReturn(true, false);
                else
                    return GroundCheckReturn(false, false);
            }
        }
        // else if(velocityCheckGrounded = true)
        else if (rb.velocity.y < 0.2 && rb.velocity.y > -0.2) //Check if the player is falling or rising. If the player's velocity doesn't change over time return grounded true
        {
            return verticalVelocityZero;
        }

        else
        {
            verticalVelocityCheck = 0;
            Debug.Log("Ray has not hit. Player is not grounded");
            if (canCoyotetime && timeSinceLastGrounded < coyoteTime)
                return GroundCheckReturn(true, false);
            else
                return GroundCheckReturn(false, false);
        }
    }

    void ExtraGroundCheck()
    {
        timeSinceLastGrounded += Time.deltaTime;

        verticalVelocityCheck += Time.deltaTime;

        if (verticalVelocityCheck > timeBetweenVerticalVelocityCheck)
        {
            if (rb.velocity.y < 0.2 && rb.velocity.y > -0.2) // Check again after time passes
            {
                verticalVelocityZero = GroundCheckReturn(true, true);
            }
            else
            {
                verticalVelocityCheck = 0;
                if (canCoyotetime && timeSinceLastGrounded < coyoteTime)
                    verticalVelocityZero = GroundCheckReturn(true, false);
                else
                    verticalVelocityZero = GroundCheckReturn(false, false);
            }
        }
        else
        {
            if (canCoyotetime && timeSinceLastGrounded < coyoteTime)
                verticalVelocityZero = GroundCheckReturn(true, false);
            else
                verticalVelocityZero = GroundCheckReturn(false, false);
        }
    }

    bool GroundCheckReturn(bool groundedReturn, bool resetGroundedTimer) // Returns grounded value and resets timer. Added this cause I ended up adding the reset timer to each true return, so we can add any extra stuff to the return values here
    {
        if (resetGroundedTimer)
        {
            timeSinceLastGrounded = 0f;
        }
        return groundedReturn;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool atLeastOneHandIsHolding = false;


    public float acceleration = 5f;
    public float deceleration = 2f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 5f;

    public Rigidbody hand1, hand2, head;
    public float armThrust = 5;
    public float baseJumpThrust = 10;
    public float maxJumpThrust = 20;

    public bool isGrabbing = false;
    public GameObject currentBlock;

    public float groundCheckDistance = 1.1f; 

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 lastMovementDirection = Vector3.forward;

    private bool isHoldingMovementInput = false;

    private bool isGrounded = true;

    // Moving platform velocity transferral
    private Rigidbody movingPlatform;
    private MovingPlatform platformMove;
    bool onPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isHoldingMovementInput)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        
        // For platform movement
        if (onPlatform)
        {
            rb.velocity = movingPlatform.velocity;
        }

        CheckGrounded();



        //keep head up (bobble head)
        head.AddForce(transform.up * armThrust);

        //grab
        if (isGrabbing)
        {
            hand1.AddForce(transform.right * armThrust);
            hand2.AddForce(transform.right * armThrust);
        }

        //rotates player toward where you are facing
        Quaternion targetRotation = Quaternion.LookRotation(lastMovementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");










        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * acceleration * Time.deltaTime;
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;

        velocity += movement;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if (horizontalInput == 0 && verticalInput == 0)
        {
            velocity -= velocity.normalized * deceleration * Time.deltaTime;
            isHoldingMovementInput = false;
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            lastMovementDirection = new Vector3(horizontalInput, 0f, verticalInput);
            isHoldingMovementInput = true;
        }


        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.JoystickButton2)) 
        {
            isGrabbing = true;
        }
        else
        {
            isGrabbing = false;
        }

        float jumpThrust = baseJumpThrust;
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            jumpThrust = Mathf.Lerp(baseJumpThrust, maxJumpThrust, 0.5f);
        }

        //jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGrounded) 
        {
            rb.AddForce(transform.up * jumpThrust, ForceMode.Impulse);
            isGrounded = false; 
        }


    }

    // raycast for ground
    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance)) 
        {
            if (hit.collider.gameObject != gameObject) // ignore the player's own collider
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    // draw raycast gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundCheckDistance);
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

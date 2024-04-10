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

    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        CheckGrounded();
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
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            lastMovementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        }



        Quaternion targetRotation = Quaternion.LookRotation(lastMovementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.JoystickButton2)) 
        {
            Grab(); 
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

        //keep head up (bobble head)
        head.AddForce(transform.up * armThrust);

        if (isGrabbing)
        {
            //currentBlock.transform.position = hand1.transform.position;
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

    // move hands
    void Grab()
    {
        hand1.AddForce(transform.right * armThrust);
        hand2.AddForce(transform.right * armThrust);
    }
}

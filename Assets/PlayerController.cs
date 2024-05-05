using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody hand1, head;


    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;

    bool isHoldingGrab = false;
    public GameObject currentBlock;

    public float acceleration = 5f;
    public float maxSpeed = 5f;
    public float deceleration = 5f;
    public float pivotSpeed = 5;

    public float headThrust = 60;
    public float armThrust = 60;

    private Vector3 velocity;
    private Vector2 lastMove;

    // Moving platform velocity transferral
    private Rigidbody movingPlatform;
    private MovingPlatform platformMove;
    bool onPlatform;

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
    }

    private void Update()
    {
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

    }


    void ApplyMovement()
    {
        Vector3 accelerationVector = new Vector3(move.x, 0, move.y).normalized * acceleration;

        //check if direction has significantly changed
        if (Vector2.Dot(lastMove.normalized, move.normalized) < 0.8f && move != Vector2.zero)
        {
           //pivot faster
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * pivotSpeed * Time.fixedDeltaTime);
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

        lastMove = move; 
    }

    void Grab()
    {
        hand1.AddForce(transform.right * armThrust);
        //hand2.AddForce(transform.right * armThrust);
    }
    
    void Jump()
    {

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

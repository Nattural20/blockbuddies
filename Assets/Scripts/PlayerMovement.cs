using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as per your requirement

    public Rigidbody hand1, hand2, head;
  
    public float armThrust = 5;
    public float jumpThrust = 10;
    public float headThrust = 100;

    public bool isGrabbing = false;

    public GameObject currentBlock;



    void Update()
    {
        // Get the user input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;


        // Rotate the movement vector relative to the camera
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f; // Ensure the player doesn't move up or down

        // Move the player
        transform.Translate(movement);


        //Forward Hand Thrust (grab)
        if (Input.GetKey(KeyCode.G))
        {
            
            hand1.AddForce(transform.right * armThrust);
            hand2.AddForce(transform.right * armThrust);
        }

        //Crazy Dance Hand Thrust
        if (Input.GetKey(KeyCode.U))
        {
            hand1.AddForce(transform.forward * armThrust);
            hand2.AddForce(-transform.forward * armThrust);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Rigidbody>().AddForce(transform.up * jumpThrust);
        }


            //keep head upright
            head.AddForce(transform.up * armThrust);



        if (isGrabbing)
        {
            //currentBlock.transform.position = hand1.transform.position;
            
        }
    }


    

}
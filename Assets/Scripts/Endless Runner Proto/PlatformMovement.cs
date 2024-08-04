using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public int speed = 2;
    public float rotation = 50;
    public float maxRotation = 15;

    private static float ZRotation = 0;


    // Update is called once per frame
    void Update()
    {
        //Platform movement
        transform.position += new Vector3(0, 0, -2) * speed * Time.deltaTime;

        //Rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            

            if (ZRotation < maxRotation)
            {
                ZRotation += rotation * Time.deltaTime;
                ZRotation = Mathf.Min(ZRotation, maxRotation);
            }
        }

        //Rotate right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (ZRotation > -maxRotation)
            {   
                ZRotation += -rotation * Time.deltaTime;
                ZRotation = Mathf.Max(ZRotation, -maxRotation);            
            }
        }

        //Reset roation to 0 when no keys are pressed
        else
        {
            ZRotation = Mathf.Lerp(ZRotation, 0, Time.deltaTime);
        }

        //Global Z axis rotation
        transform.rotation = Quaternion.Euler(0, 0, ZRotation);
    }



}

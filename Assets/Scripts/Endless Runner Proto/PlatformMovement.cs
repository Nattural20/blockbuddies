using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public int speed = 3;
    public float rotation = 50;
    public float maxRotation = 15;

    private static float ZRotation = 0;
    private static float XMovement =1;


    // Update is called once per frame
    void Update()
    {
        //Platform z axis movement
        transform.position += new Vector3(0, 0, -2) * speed * Time.deltaTime;

        //Movement left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            //Rotation
            if (ZRotation < maxRotation)
            {
                ZRotation += rotation * Time.deltaTime;
                ZRotation = Mathf.Min(ZRotation, maxRotation);
            }

            //X axis movement
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        //Movement right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotation
            if (ZRotation > -maxRotation)
            {   
                ZRotation += -rotation * Time.deltaTime;
                ZRotation = Mathf.Max(ZRotation, -maxRotation);            
            }

            //X Axis movement
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
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

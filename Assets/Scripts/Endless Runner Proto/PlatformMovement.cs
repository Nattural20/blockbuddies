using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public float speed = 3;
    public float rotation = 50;
    public float maxRotation = 15;
    

    private static float ZRotation = 0;
    private static float XMovement =1;

    [SerializeField]
    private float raftDirection;
    public ArduinoReaderSpoof readerScript;

    private void Start()
    {
        GameObject theObject = GameObject.Find("Arduino Component Spoof");
        readerScript = theObject.GetComponent<ArduinoReaderSpoof>();
    }

    // Update is called once per frame
    void Update()
    {
        //Platform z axis movement
        transform.position += new Vector3(0, 0, -2) * speed * Time.deltaTime;

        raftDirection = CheckInput();


        //Movement right
        if (Input.GetKey(KeyCode.RightArrow) || (raftDirection < 0))
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

        //Movement left
        if (Input.GetKey(KeyCode.LeftArrow) || (raftDirection > 0))
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
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, ZRotation);


    }

    private float CheckInput()
    {
        int[] outputArray = readerScript.OutputArray;
        float input = 0;
        Debug.Log("Direction:" + raftDirection);
        if (outputArray[1] == 1) input++;
        if (outputArray[4] == 1) input++;
        if (outputArray[7] == 1) input++;
        if (outputArray[3] == 1) input--;
        if (outputArray[6] == 1) input--;
        if (outputArray[9] == 1) input--;
        return input;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    
    public float rotation = 50;
    public float maxRotation = 15;

    private static float ZRotation = 0;

    [SerializeField]
    private float raftDirection;
    public ArduinoReaderSpoof readerScript;


    // Update is called once per frame
    void Update()
    {
        raftDirection = CheckInput();

        //Rotate left
        if (Input.GetKey(KeyCode.LeftArrow) || (raftDirection < 0))
        {
            if (ZRotation < maxRotation)
            {
                ZRotation += rotation * Time.deltaTime;
                ZRotation = Mathf.Min(ZRotation, maxRotation);
            }
        }

        //Rotate right
        if (Input.GetKey(KeyCode.RightArrow) || (raftDirection > 0))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class ArduinoReader : MonoBehaviour
{
    Thread t = new Thread(ArduinoGetter.MyThreadLoop);

    public char[] OutputArray;
    char [] previousOutput;
    // Start is called before the first frame update
    //This code starts a thread and returns its array as a value in ArduinoGetter.cs
    void Start()
    {
        t.Start();
        Debug.Log("Started Reading from ArduinoGetter class");
        previousOutput = new char[10];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ArduinoGetter.PhysicalBlockState);
        if (ArduinoGetter.PhysicalBlockState != null)
        {
            OutputArray = ArduinoGetter.PhysicalBlockState;
            previousOutput = OutputArray;
        }
        else 
        {
            OutputArray = previousOutput;
        }
    }

    void OnDestroy() //ALWAYS invoke OnDestroy when using Threads- THEY DO NOT CLOSE WHEN YOU STOP THE EDITOR!!! Your PC will EXPLODE!!!! VIOLENTLY!!!!
    {
        t.Abort();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class ArduinoReader : MonoBehaviour
{
    Thread t = new Thread(ArduinoGetter.MyThreadLoop);

    public char[] OutputArray;
    // Start is called before the first frame update
    void Start()
    {
        t.Start();
        Debug.Log("Started Reading from ArduinoGetter class");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ArduinoGetter.PhysicalBlockState);
        OutputArray = ArduinoGetter.PhysicalBlockState;
    }

    void OnDestroy() //ALWAYS invoke OnDestroy when using Threads- THEY DO NOT CLOSE WHEN YOU STOP THE EDITOR!!! Your PC will EXPLODE!!!! VIOLENTLY!!!!
    {
        t.Abort();
    }
}

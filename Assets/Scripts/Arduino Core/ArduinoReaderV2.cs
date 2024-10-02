using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ArduinoReaderV2 : MonoBehaviour
{
    private Thread t;
    private CancellationTokenSource cancellationTokenSource;

    public char[] OutputArray;
    char[] previousOutput;

    // Start is called before the first frame update
    void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        StartArduinoThread();
        Debug.Log("Started Reading from ArduinoGetter class");
        previousOutput = new char[10];
    }

    void StartArduinoThread()
    {
        // Start a new thread using the cancellation token
        t = new Thread(() => ArduinoGetterV2.MyThreadLoop(cancellationTokenSource.Token));
        t.Start();
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnDestroy()
    {
        StopArduinoThread();
    }

    // Gracefully stop the thread using cancellation
    void StopArduinoThread()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            t.Join(); // Wait for the thread to finish
            Debug.Log("Thread stopped.");
        }
    }

    public void ForceThreadReset()
    {
        StopArduinoThread(); // Stop the current thread safely
        cancellationTokenSource = new CancellationTokenSource(); // Create a new token
        StartArduinoThread(); // Start a new thread
        Debug.Log("Thread restarted.");
    }
}


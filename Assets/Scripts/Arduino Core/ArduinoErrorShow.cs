using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArduinoErrorShow : MonoBehaviour
{
    public ArduinoReader reader;
    private void Start()
    {
        StartCoroutine(CheckArduinoDelay());
    }
    //private void Update()
    //{
    //    CheckArduino();
    //}
    void CheckArduino()
    {
        foreach (char input in reader.OutputArray)
        {
            if (input.ToString() != "0" && input.ToString() != "1")
            {
                GetComponent<Image>().color = Color.red;
            }
        }
    }
    IEnumerator CheckArduinoDelay()
    {
        yield return new WaitForSeconds(1);
        CheckArduino();
        Destroy(this);
    }
    IEnumerator StopChecking()
    {
        yield return new WaitForSeconds(1);
    }
}

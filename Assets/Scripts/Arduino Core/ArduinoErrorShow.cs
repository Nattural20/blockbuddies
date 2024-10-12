using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArduinoErrorShow : MonoBehaviour
{
    public ArduinoReader reader;
    public ResetManager reset;

    bool doAutoReset = true;
    private void Start()
    {
        if (Application.isEditor)
        {
            doAutoReset = false;
        }
        StartCoroutine(CheckArduinoDelay());
    }
    bool CheckArduino()
    {
        bool allOkay = true;
        foreach (char input in reader.OutputArray)
        {
            if (input.ToString() != "0" && input.ToString() != "1")
            {
                GetComponent<Image>().color = Color.red;
                allOkay = false;
            }
        }
        return allOkay;
    }
    IEnumerator CheckArduinoDelay()
    {
        yield return new WaitForSeconds(1);
        if (CheckArduino() == false && doAutoReset)
        {
            reset.SingleReset();
        }
        Destroy(this);
    }
}

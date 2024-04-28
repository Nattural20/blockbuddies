using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class ArduinoReaderSpoof : MonoBehaviour
{
    public Image[] spockDisplay;
    public int[] OutputArray;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {if (OutputArray[3] == 1){OutputArray[3] = 0; spockDisplay[2].color = Color.red;} else{OutputArray[3] = 1; spockDisplay[2].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {if (OutputArray[2] == 1){OutputArray[2] = 0; spockDisplay[1].color = Color.red;} else{OutputArray[2] = 1; spockDisplay[1].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {if (OutputArray[1] == 1){OutputArray[1] = 0; spockDisplay[0].color = Color.red;} else{OutputArray[1] = 1; spockDisplay[0].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {if (OutputArray[6] == 1){OutputArray[6] = 0; spockDisplay[5].color = Color.red;} else{OutputArray[6] = 1; spockDisplay[5].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {if (OutputArray[5] == 1){OutputArray[5] = 0; spockDisplay[4].color = Color.red;} else{OutputArray[5] = 1; spockDisplay[4].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {if (OutputArray[4] == 1){OutputArray[4] = 0; spockDisplay[3].color = Color.red;} else{OutputArray[4] = 1; spockDisplay[3].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad7)) {if (OutputArray[9] == 1){OutputArray[9] = 0; spockDisplay[8].color = Color.red;} else{OutputArray[9] = 1; spockDisplay[8].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {if (OutputArray[8] == 1){OutputArray[8] = 0; spockDisplay[7].color = Color.red;} else{OutputArray[8] = 1; spockDisplay[7].color = Color.green;} }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {if (OutputArray[7] == 1){OutputArray[7] = 0; spockDisplay[6].color = Color.red;} else{OutputArray[7] = 1; spockDisplay[6].color = Color.green;} }
    }
}

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
    public GameObject[] ghostSpocks;
    public ParticleSystem griddyPlace;

    void Update()
    {    
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) {if (OutputArray[1] == 1){OutputArray[1] = 0; spockDisplay[0].color = Color.red; ghostSpocks[0].SetActive(false) ;} else{OutputArray[1] = 1; spockDisplay[0].color = Color.green; ghostSpocks[0].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) {if (OutputArray[2] == 1){OutputArray[2] = 0; spockDisplay[1].color = Color.red; ghostSpocks[1].SetActive(false) ;} else{OutputArray[2] = 1; spockDisplay[1].color = Color.green; ghostSpocks[1].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) {if (OutputArray[3] == 1){OutputArray[3] = 0; spockDisplay[2].color = Color.red; ghostSpocks[2].SetActive(false) ;} else{OutputArray[3] = 1; spockDisplay[2].color = Color.green; ghostSpocks[2].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) {if (OutputArray[4] == 1){OutputArray[4] = 0; spockDisplay[3].color = Color.red; ghostSpocks[3].SetActive(false) ;} else{OutputArray[4] = 1; spockDisplay[3].color = Color.green; ghostSpocks[3].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) {if (OutputArray[5] == 1){OutputArray[5] = 0; spockDisplay[4].color = Color.red; ghostSpocks[4].SetActive(false) ;} else{OutputArray[5] = 1; spockDisplay[4].color = Color.green; ghostSpocks[4].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) {if (OutputArray[6] == 1){OutputArray[6] = 0; spockDisplay[5].color = Color.red; ghostSpocks[5].SetActive(false) ;} else{OutputArray[6] = 1; spockDisplay[5].color = Color.green; ghostSpocks[5].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) {if (OutputArray[7] == 1){OutputArray[7] = 0; spockDisplay[6].color = Color.red; ghostSpocks[6].SetActive(false) ;} else{OutputArray[7] = 1; spockDisplay[6].color = Color.green; ghostSpocks[6].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) {if (OutputArray[8] == 1){OutputArray[8] = 0; spockDisplay[7].color = Color.red; ghostSpocks[7].SetActive(false) ;} else{OutputArray[8] = 1; spockDisplay[7].color = Color.green; ghostSpocks[7].SetActive(true); griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) {if (OutputArray[9] == 1){OutputArray[9] = 0; spockDisplay[8].color = Color.red; ghostSpocks[8].SetActive(false) ;} else{OutputArray[9] = 1; spockDisplay[8].color = Color.green; ghostSpocks[8].SetActive(true); griddyPlace.Play(); } }
    }
}
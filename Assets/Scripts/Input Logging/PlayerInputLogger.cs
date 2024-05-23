using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Buffers;


public class PlayerInputLogger : MonoBehaviour
{
    // Start is called before the first frame update
    public string fileName = "PlayerInputLog.txt";

    public GameObject player;
    

    private string fullPath;
    public int arrayPos;
    public GameObject[] blocks;

    private string logDelimiter = ", ";

    private PlayerMovement playerMovement;
    private ArduinoReader arduinoReader;
    private bool buttonPressed;
    public float timer = 5f;
    private float resetTimer = 5f;

    private char[] OutputArray;

    public void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        arduinoReader = player.GetComponent<ArduinoReader>();

        fullPath = Path.Combine(Application.persistentDataPath, fileName);
        
        Debug.Log(fullPath);

        File.WriteAllText(fullPath, "Timestamp, Action, PsitionX, PositionY, PositionZ, Spock Position\n");
    }

    private void Update()
    {
        
        timer = timer - Time.deltaTime;
        
        char[] input = GetComponent<ArduinoReader>().OutputArray;

        if(timer < 0)
        {       
 
            timer = resetTimer;
            LogPlayerInput("Movement", "Input");
            
        }
        if (input[0].ToString() == "1")
        {
            LogPlayerInput("Movement", "Input");
        }
    }



    public void LogPlayerInput(string action, string spocks)
    {
        Vector3 getMovement = player.transform.position;

        char[] input = GetComponent<ArduinoReader>().OutputArray;

        string gridConfig = "";
        for (int i = 9; i >= 0; i--)
        {
            gridConfig += (input[i] == 1) ? "[x]" : "[-]";
            if (i % 3 == 0) gridConfig += "\n";
            else gridConfig += " ";
        }

        string logEntry = $"{System.DateTime.Now.ToString("HH-mm-ss")}{logDelimiter}{action}{logDelimiter}{getMovement.x}{logDelimiter}{getMovement.y}{logDelimiter}{getMovement.z}{logDelimiter}{input}{"\n"}";

        File.AppendAllText(fullPath, logEntry);
    }
}

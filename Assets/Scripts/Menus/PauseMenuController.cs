using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    public GameObject spawnSelect;
    private Dropdown actualspawnSelect;
    public List<Transform> spawns;
    public List<GameObject> playerParts;
    public Camera mainCam;

    public GameObject fpsCounter;
    public GameObject pauseMenuUI;
    public bool isPaused = false;

    private bool fpsOn = false;



    private void Awake()
    {




    }

    private void Start()
    {
        fpsCounter.SetActive(false);

        actualspawnSelect = spawnSelect.GetComponent<Dropdown>();
        actualspawnSelect.onValueChanged.AddListener(OnSpawnLocationsSelect);
        if (pauseMenuUI == null)
        {
            Debug.Log("I'm throwing a fit cause I can't read that I have an object.");
        }
        else
        {
            //Turns off pause menu at game start
            pauseMenuUI.SetActive(false);
        }

        
        PopulateDropdown();
    }

    void Update()
    {
        
    }

    

    public void PauseMenu()
    {
        /*var pm = new PauseMenuController();*/
        if (isPaused)
        {
            Resume();
        }
        else
        { 
            Pause();
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    void PopulateDropdown()
    {
        actualspawnSelect.ClearOptions();

        List<string> options = new List<string>();
        foreach(var spawnzone in spawns)
        {
            options.Add(spawnzone.name);
        }

        actualspawnSelect.AddOptions(options);
    }


    public void OnSpawnLocationsSelect(int index)
    {

        if (index >= 0 && index < spawns.Count)
        {
            Vector3 selectedPostion = spawns[index].position;

            foreach (GameObject player in playerParts)
            {
                player.transform.position = selectedPostion;
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb = null)
                {
                    rb.velocity = Vector3.zero;
                }
            }

            Debug.Log("Selected location: " + selectedPostion);
        }
        else
        {
            Debug.LogWarning("Invalid index selected: " +  index);
        }
    }

    public void FPSCheck()
    {
        if (fpsOn == false)
        {
            fpsCounter.SetActive(true);
            fpsOn = true;
        }

        else
        {
            fpsCounter.SetActive(false);
            fpsOn = false;
        }
    }

}

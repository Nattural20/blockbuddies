using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PauseMenuController : MonoBehaviour
{
  
    public Dropdown spawnSelect;
    public List<Transform> spawns;
    public List<GameObject> playerParts;
    
    public GameObject pauseMenuUI;
    private bool isPaused = false;


    private void Start()
    {
        //Turns off pause menu at game start
        pauseMenuUI.SetActive(false);

        PopulateDropdown();

        spawnSelect.onValueChanged.AddListener(OnSpawnLocationsSelect);
    }

    void Update()
    {

        //Pauses an unpauses game on ESC press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
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
        spawnSelect.ClearOptions();

        List<string> options = new List<string>();
        foreach(var spawnzone in spawns)
        {
            options.Add(spawnzone.name);
        }

        spawnSelect.AddOptions(options);
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

}

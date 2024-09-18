using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetManager : MonoBehaviour
{

    private static ResetManager instance;

    public Dropdown dropdown;  // Your dropdown component
    private static int savedDropdownValue = -1;  // Static variable to store value

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (dropdown == null)
        {
            dropdown = GameObject.Find("Spawner Rotation").GetComponent<Dropdown>();

            DontDestroyOnLoad(dropdown.gameObject);
        }
        
        // If there's a saved value, use it to set the dropdown
        if (savedDropdownValue != -1)
        {
            dropdown.value = savedDropdownValue;
        }

        // Add listener to update the static variable when the value changes
        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
        
    }

    public void SaveDropdownValue()
    {
        // Store the dropdown's current value in a static variable
        savedDropdownValue = dropdown.value;
    }

    public void ResetScene()
    {
        Debug.Log("BOISJDFBJSDBFJDSB");
        StartCoroutine(RespawnLag());

    }

    IEnumerator RespawnLag()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("Waiting 1 second");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Second Reset");
        Time.timeScale = 1f;
    }

    public void ApplyStoredRotation()
    {
        // Get the stored orientation
        SpawnerRotationManager.Orientation orientation = SpawnerRotationManager.Instance.CurrentOrientation;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign dropdown when the scene is loaded
        dropdown = GameObject.Find("Spawner Rotation").GetComponent<Dropdown>();

        // Reapply the saved value
        if (savedDropdownValue != -1)
        {
            dropdown.value = savedDropdownValue;
        }

        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

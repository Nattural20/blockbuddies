using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetManager : MonoBehaviour
{

    private static ResetManager instance;

    public Dropdown dropdown;  // The specific dropdown component
    private static int savedDropdownValue = -1;  // Static variable to store value across scenes

    public Toggle spoof;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist this object across scenes
        }
/*        else
        {
            Destroy(gameObject);  // Ensure only one instance of ResetManager exists
        }*/
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (dropdown == null)
        {
            dropdown = GameObject.Find("Spawner Rotation").GetComponent<Dropdown>();
            DontDestroyOnLoad(dropdown.gameObject);  // Ensure the dropdown is not destroyed
        }

        // Load the saved dropdown value from PlayerPrefs or use the static value if it exists
        if (savedDropdownValue == -1)
        {
            savedDropdownValue = PlayerPrefs.GetInt("Rotation", dropdown.value);
        }

        // Set the dropdown to the saved value
        dropdown.value = savedDropdownValue;

        // Add listener to save the dropdown value when it changes
        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
    }

    public void SaveDropdownValue()
    {
        // Save the dropdown's current value to the static variable and PlayerPrefs
        savedDropdownValue = dropdown.value;
        PlayerPrefs.SetInt("Rotation", savedDropdownValue);
        PlayerPrefs.Save();  // Force PlayerPrefs to save immediately
    }

    public void ResetScene()
    {
        StartCoroutine(RespawnLag());
        Time.timeScale = 1f;
    }

    IEnumerator RespawnLag()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the dropdown after the scene is loaded
        dropdown = GameObject.Find("Spawner Rotation").GetComponent<Dropdown>();

        // Reapply the saved value
        dropdown.value = savedDropdownValue;

        // Ensure the listener is added to save the value on change
        dropdown.onValueChanged.RemoveAllListeners();  // Remove existing listeners to avoid duplicates
        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

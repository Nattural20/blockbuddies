using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{

    private static ResetManager instance;


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
}

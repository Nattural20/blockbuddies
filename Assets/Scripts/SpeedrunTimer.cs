using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    public float timer;
    public bool timerRunning;
    void Update()
    {
        if (timerRunning)
            timer += Time.deltaTime;
    }
}

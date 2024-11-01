using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerGet : MonoBehaviour
{
    public SpeedrunTimer timer;

    TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        int mathTime = ((int)timer.timer);
        float seconds = mathTime % 60;
        float minutes = mathTime / 60;
        string time = minutes + ":" + seconds;
        text.text = time;
    }
}

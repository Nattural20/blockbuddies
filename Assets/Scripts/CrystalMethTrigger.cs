using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMethTrigger : MonoBehaviour
{
    public bool startTimer;
    SpeedrunTimer timer;
    private void Start()
    {
        timer = GetComponentInParent<SpeedrunTimer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            timer.timerRunning = startTimer;
            Destroy(gameObject);
        }
    }
}

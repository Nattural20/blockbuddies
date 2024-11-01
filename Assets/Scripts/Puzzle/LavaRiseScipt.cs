using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRiseScipt : MonoBehaviour
{
    public float riseSpeed;
    public bool rising;

    public Vector3 resetPosition;

    public ArduinoLockedSpawn[] spawns;
    private void Start()
    {
        resetPosition = Vector3.zero;
    }
    void FixedUpdate()
    {
        if (rising)
        {
            transform.Translate(0, riseSpeed * Time.fixedDeltaTime, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            ResetVines();
            other.gameObject.GetComponent<SetPos>().TeleportPlayer();
        }
    }
    public void ResetVines()
    {
        rising = false;
        transform.localPosition = resetPosition;

        foreach (ArduinoLockedSpawn spawn in spawns)
        {
            spawn.RemoveSpocks();
        }
    }
}

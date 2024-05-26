using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRiseScipt : MonoBehaviour
{
    public float riseSpeed;
    public bool rising;
    void Update()
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
            var player = other.gameObject;
            player.GetComponent<SetPos>().TeleportPlayer();
            transform.localPosition = Vector3.zero;
        }
    }
}

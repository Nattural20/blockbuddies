using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaProximity : MonoBehaviour
{
    public bool toggleLavaBubbles = true;
    public LavaBubble[] lavaBubbles;

    public bool toggleGeyserGate = false;
    public GameObject[] geyserGates;

    private void Start()
    {
        if (lavaBubbles.Count() == 0)
        {
            lavaBubbles = GetComponents<LavaBubble>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            Debug.Log("2");
            if (toggleLavaBubbles)
            {
                foreach (var bubble in lavaBubbles)
                    bubble.ActivateBubble();
                Debug.Log("LAVA BUBBLE");
            }
            if (toggleGeyserGate)
            {
                foreach (GameObject gate in geyserGates)
                {
                    if (gate.activeSelf)
                    {
                        gate.SetActive(false);
                        Debug.Log(gate + "Turned off");
                    }else if (!gate.activeSelf)
                    {
                        gate.SetActive(true);
                        Debug.Log(gate + "Turned on");
                    }
                    Debug.Log(gate);
                }
                   
                //Debug.Log("Gate");
            }
            this.GetComponent<BoxCollider>().enabled = false;
        }
        ///Debug.Log("1");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            foreach (var bubble in lavaBubbles)
            {
                bubble.DeactivateBubble();
            }
        }
    }
}

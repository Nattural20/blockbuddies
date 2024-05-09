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
            if (toggleLavaBubbles)
            {
                foreach (var bubble in lavaBubbles)
                    bubble.ActivateBubble();
            }
            if (toggleGeyserGate)
            {
                foreach (var gate in geyserGates)
                    gate.SetActive(!gate.activeSelf);
            }
        }
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

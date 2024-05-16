using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaProximityTimed : MonoBehaviour
{
    public bool toggleLavaBubbles = true;
    public LavaBubble[] lavaBubbles;

    public bool toggleGeyserGate = false;
    public GameObject[] geyserGates;

    /// <summary>
    /// WIP Script. Limited functionality but it does delay nearby geysers. 
    /// </summary>
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
                        StopCoroutine(LavaSequence(gate));
                        gate.SetActive(false);
                        Debug.Log(gate + "Turned off");
                    }else if (!gate.activeSelf)
                    {
                        StartCoroutine(LavaSequence(gate));
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

    //    IEnumerator LavaSequence(GameObject[] gates)
    //    {
    //        while (true)
    //        {
    //            foreach (GameObject gate in gates)
    //            {
    //                // Code before the first yield
    //                Debug.Log("Lava started");

    //                // Pause the execution for 1 second
    //                yield return new WaitForSeconds(1.5f);
    //                // Code after the wait
    //                Debug.Log("Firing Lava");
    //                gate.SetActive(true);
    //                yield return new WaitForSeconds(1.5f);
    //            }

    //        }

    //        // Coroutine ends
    //    }
    IEnumerator LavaSequence(GameObject gate)
    {
        while (true)
        {

            // Get all child objects of the current 'gate' object
            Transform[] childTransforms = gate.GetComponentsInChildren<Transform>(true);

            Debug.Log("Lava started for gate: " + gate.name);

            int index = 0;
                
            while(index < childTransforms.Length)
            {

                childTransforms[index].gameObject.SetActive(true);
                // Activate the child GameObject
                //Debug.Log("Firing Lava for child: " + childTransforms[index].gameObject.name);

                Debug.Log("Firing Lava Object: " + index + "...from " + gate);
                index++;

                //delay next child toggle
                yield return new WaitForSeconds(10f);
                childTransforms[index].gameObject.SetActive(false);
            }

        }
    }


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneDetect : MonoBehaviour
{
    public RuneSolve solve;
    public GameObject[] solutionHint;
    public Material hintActive;

    private Material hintInactive;
    private GameObject spock;

    bool activated = false;
    private void Start()
    {
        hintInactive = solutionHint[0].GetComponent<MeshRenderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spocks"))
        {
            if (!activated)
            {
                solve.runesSolved += 1;
                activated = true;
                spock = other.gameObject;
                foreach (GameObject hint in solutionHint)
                {
                    hint.GetComponent<MeshRenderer>().material = hintActive;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Spocks"))
        {
            if (other.gameObject == spock)
            {
                solve.runesSolved -= 1;
                activated = false;
                spock = null;
                foreach (GameObject hint in solutionHint)
                {
                    hint.GetComponent<MeshRenderer>().material = hintInactive;
                }
            }
        }
    }
    private void Update()
    {
        if (spock != null)
        {
            spock.transform.position = Vector3.Lerp(spock.transform.position, transform.position, 5*Time.deltaTime);
        }
    }
}

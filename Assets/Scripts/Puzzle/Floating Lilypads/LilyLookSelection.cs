using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyLookSelection : MonoBehaviour
{
    public LayerMask jimCarreys;
    public float zoneDistance, centerDistance;
    public bool onScreen;
    LilyPadLockedSpawn lilyPad;
    Camera cam;
    Vector2 screenSize;
    Vector3 screenMiddle;

    Material playerHere, playerNotHere;
    void Start()
    {
        lilyPad = GetComponentInParent<LilyPadLockedSpawn>();
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;
        screenMiddle = screenSize / 2;
        cam = FindObjectOfType(typeof(Camera)) as Camera;

        playerNotHere = GetComponent<MeshRenderer>().material;
        playerHere = new Material(GetComponent<MeshRenderer>().material);
        playerHere.color = Color.red;
    }

    void Update()
    {
        var screenPos = cam.WorldToScreenPoint(transform.position);

        if (screenPos.x > 0 && screenPos.x < screenSize.x && screenPos.y > 0 && screenPos.y < screenSize.y && screenPos.z > 0) // If it is on screen, and not behind the camera
        {
            onScreen = true;
            GetComponent<MeshRenderer>().material = playerHere;
            zoneDistance = screenPos.z;
            centerDistance = Vector2.Distance(screenMiddle, screenPos);


            lilyPad.onScreen = true;
            lilyPad.padDistance = screenPos.z;
            lilyPad.centerDistance = centerDistance;
            //Debug.Log(lilyPad.name + " is on screen at " + screenPos);
        }
        else
        {
            onScreen = false;

            GetComponent<MeshRenderer>().material = playerNotHere;

            lilyPad.onScreen = false;
        }
    }
}

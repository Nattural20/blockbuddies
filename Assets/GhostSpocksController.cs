using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class GhostSpocksController : MonoBehaviour
{
    public bool increasingDistance, decreasingDistance;

    public float spockRotation = 0;

    public Transform refPoint;
    public Transform player;
    public Transform ghostSpocks;

    public int maxYHeight = 3, minYHeight = 0, newYHeight;
    public float ghostDistanceMax = 7, ghostDistanceMin = 3, ghostDistance = 5, ghostMoveSpeed = 5;

    PlayerControls controls;
    [SerializeField] public Dropdown spawnRotation;
    public readonly string[] options = { "Left", "Up", "Right", "Down" };

    private void Awake()
    {
        //controls = new PlayerControls();

        //ghostbusters starring bill murray except the stay puft marshmallow man has won cause idk what is not happening here
        //controls.Gameplay.GhostHeightIncrease.performed += ctx => IncreaseGhostHeight();
        //controls.Gameplay.GhostHeightDecrease.performed += ctx => IncreaseGhostHeight();

    }
    void FixedUpdate()
    {
        transform.position = player.position;
        //this.transform.position = new Vector3(refPoint.transform.position.x, player.position.y, refPoint.transform.position.z);
        this.transform.rotation = Quaternion.Euler(0, refPoint.transform.rotation.eulerAngles.y-90, 0);

        ghostSpocks.position = transform.position + transform.right * ghostDistance + new Vector3(0, newYHeight, 0);


        
            if (spawnRotation.value == 0)
            {
                spockRotation = 0;
            }
            else if (spawnRotation.value == 1)
            {
                spockRotation = 90;
            }
            else if (spawnRotation.value == 2)
            {
                spockRotation = 180;
            }
            else if (spawnRotation.value == 3)
            {
                spockRotation = 270;
            }
        
        else 
        {
            Debug.Log("cry");
        }

        ghostSpocks.rotation = transform.rotation * Quaternion.Euler(1, spockRotation, 1);

        if (increasingDistance)
        {
            SpiritedAway();
        }

        if (decreasingDistance)
        {
            SpiritedCloser();
        }

    }

    void Update() //< /\ > \/
    {

        /*
        if (Input.GetAxis("L2") > 0.1f) //Use a custom axis input for the triggers
        {
            SpiritedCloser();
        }
        if (Input.GetAxis("R2") > 0.1f) //Use a custom axis input for the triggers
        {
            SpiritedAway();
        }
        */
        
    }
    public void IncreaseGhostHeight() // paul blart mall cop
    {
        Debug.Log("increase height");
        if (newYHeight < maxYHeight)
        {
            newYHeight++;
        }
    }
    public void UnIncreaseGhostHeight() //paul cop mall blart
    {

        Debug.Log("de height");
        if (newYHeight > minYHeight)
        {
            
            newYHeight--;
        }

    }
    public void SpiritedAway() // mall blart cop plart blart
    {

        if (ghostDistance < ghostDistanceMax)
        {
            Debug.Log("increase dis");
            ghostDistance += Time.deltaTime * ghostMoveSpeed;
        }
      
    }
    public void SpiritedCloser() // paul cop mlart cop blart paul mall cop blart paul
    //Ok
    {
        Debug.Log("de dis");
        if (ghostDistance > ghostDistanceMin)
            ghostDistance -= Time.deltaTime * ghostMoveSpeed;
    }
}

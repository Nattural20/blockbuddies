using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyPlayerPos : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = player.position;
    }
}
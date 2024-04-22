using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour   
{
    public bool hasJoint = false;

    // Start is called before the first frame update
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().CompareTag("Spocks") && !hasJoint)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
            hasJoint = true;
        }

       
    }

    void ScriptDestruction()
    {
        Destroy(this, 0.00001f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour   
{
    public bool hasJoint = false;

    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(ScriptDestruction());
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().CompareTag("Spocks") && !hasJoint)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
            hasJoint = true;
        }

       
    }

    IEnumerator ScriptDestruction()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this);
    }
}

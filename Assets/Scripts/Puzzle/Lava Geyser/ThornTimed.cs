using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class ThornTimed : MonoBehaviour
{
    private GameObject _affectedSpock;
    public float thornForce = 50;
    private bool _isTriggered;
    public float thornsTimer = 3f;

    private Vector3 setPosition, risePositon;

    // Start is called before the first frame update
    void Start()
    {
        setPosition = transform.position;
        risePositon = transform.position + new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider col)
    {///Vocal cords Ready
        if (!_isTriggered)
        {
            if (col.CompareTag("Spocks") || col.CompareTag("Lava Spock"))
            {
                _isTriggered = true;
                _affectedSpock = col.gameObject;
                StartCoroutine(ThornExplode(_affectedSpock));
                Debug.Log("1st trigger Triggered. " + _affectedSpock);
            }
        }

    }

    IEnumerator ThornExplode(GameObject spock)
    { ///let the show Begin
        //Debug.Log("trigger Triggered");
        //yield return new WaitForSeconds(thornTimer);
        //Debug.Log("Timer Done");

        transform.position = risePositon;

        //check for sinking spocks
        if (spock.CompareTag("Lava Spock"))
        {
            spock.GetComponent<LavaSpockScript>().enabled = false;

            spock.GetComponent<Rigidbody>().isKinematic = false;

            spock.GetComponent<Rigidbody>().AddForce(new Vector3(0, thornForce, 0), ForceMode.Impulse);
            //****NOTE: THIS Only effects one GameObject Spock at a time- in future, look towards adjusting to take in an array and affect all of them. 
        }
        else if (spock.CompareTag("Spocks"))
        {
            spock.GetComponent<Rigidbody>().AddForce(new Vector3(0, thornForce, 0), ForceMode.Impulse);
        }

        yield return new WaitForSeconds(thornsTimer);
        transform.position = setPosition;
        _isTriggered = false;

        spock.tag = "Spocks";
    }
}

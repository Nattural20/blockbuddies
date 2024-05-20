using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class ThornTimed : MonoBehaviour
{
    private GameObject _affectedSpock;
    private GameObject _thorn;
    public float thornForce = 50;
    private bool _isTriggered;
    public float thornTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _thorn = this.gameObject;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider col)
    {///Vocal cords Ready
        if (!_isTriggered)
        {
            _isTriggered = true;
            _affectedSpock = col.gameObject;
            StartCoroutine(ThornExplode(_affectedSpock));
            Debug.Log("1st trigger Triggered. " + _affectedSpock);
        }

    }

    IEnumerator ThornExplode(GameObject spock)
    { ///let the show Begin
        Debug.Log("trigger Triggered");
        while (_isTriggered)
        {
            Debug.Log("trigger Triggered");
            yield return new WaitForSeconds(thornTimer);
            Debug.Log("Timer Done");
            //check for sinking spocks
            if (spock.CompareTag("Lava Spock"))
            {
                spock.GetComponent<LavaSpockScript>().enabled = false;
                
                spock.GetComponent<Rigidbody>().isKinematic = false;
                //****NOTE: THIS Only effects one GameObject Spock at a time- in future, look towards adjusting to take in an array and affect all of them. 
            }

            if (spock.CompareTag("Spocks"))
            {
                spock.GetComponent<Rigidbody>().AddForce(new Vector3(0, thornForce, 0), ForceMode.Impulse);
            }
            if (spock.CompareTag("Lava Spock"))
            {
                spock.GetComponent<Rigidbody>().AddForce(new Vector3(0, thornForce, 0), ForceMode.Impulse);
                yield return new WaitForSeconds(2f);
                spock.tag = "Spocks";
            }

            _isTriggered = false;
            
        }

    }
}

using System;
using System.Collections;
using UnityEngine;

public class ThornTimed : MonoBehaviour
{
    private GameObject _affectedSpock;
    public float thornForce = 50;
    private bool _isTriggered;
    public float thornsTimer = 3f;

    private Vector3 setPosition, risePosition, spornPosition;
    private float riseSpeed = 10f;
    private bool isRising = false, sporning = false;

    void Start()
    {
        setPosition = transform.position;
        risePosition = transform.position + new Vector3(0, 5, 0);
    }

    void Update()
    {
        if (isRising)
        {
            transform.position = Vector3.Lerp(transform.position, risePosition, riseSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, risePosition) < 0.1f)
            {
                isRising = false;
            }
        }
        if (sporning)
        {
            transform.parent.transform.position = Vector3.Lerp(transform.parent.position, spornPosition, 5 * Time.deltaTime);
            if (Vector3.Distance(transform.parent.position, spornPosition) < 0.1f)
            {
                sporning = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!_isTriggered)
        {
            if (col.CompareTag("Spocks") || col.CompareTag("Lava Spock") || col.CompareTag("River Spock"))
            {
                _isTriggered = true;
                _affectedSpock = col.gameObject;
                StartCoroutine(ThornExplode(_affectedSpock));
                Debug.Log("1st trigger Triggered. " + _affectedSpock);
            }
        }
    }

    IEnumerator ThornExplode(GameObject spock)
    {
        isRising = true;

        if (spock.CompareTag("Lava Spock"))
        {
            Destroy(spock.GetComponent<LavaSpockScript>());
            spock.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (spock.CompareTag("River Spock"))
        {
            Destroy(spock.GetComponent<RiverSpockScript>());
        }
        var launchForce = new Vector3(UnityEngine.Random.value, thornForce, UnityEngine.Random.value);

        spock.GetComponent<Rigidbody>().velocity = launchForce;

        yield return new WaitForSeconds(thornsTimer);
        transform.position = setPosition;
        _isTriggered = false;
        spock.tag = "Spocks";
    }

    public void ThawnSporn()
    {
        spornPosition = transform.parent.position;

        var downSet = UnityEngine.Random.Range(2, 8) + UnityEngine.Random.value;
        
        transform.parent.transform.position -= new Vector3(0, downSet, 0);
        sporning = true;
    }
}

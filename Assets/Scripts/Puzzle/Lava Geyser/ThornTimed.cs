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
    bool unrising;

    void Start()
    {
        //setPosition = transform.position;
        //risePosition = transform.position + new Vector3(0, 5, 0);
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
        if (unrising)
        {
            transform.position = Vector3.Lerp(transform.position, setPosition, riseSpeed * 2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, setPosition) < 0.1f)
            {
                unrising = false;
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
        if (!_isTriggered && !sporning)
        {
            if (col.CompareTag("Spocks") || col.CompareTag("Lava Spock") || col.CompareTag("River Spock"))
            {
                _isTriggered = true;
                _affectedSpock = col.gameObject;
                StartCoroutine(ThornExplode(_affectedSpock));
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
        unrising = true;

        yield return new WaitForSeconds(thornsTimer/2);
        _isTriggered = false;
        spock.tag = "Spocks";
    }

    public void ThawnSporn()
    {
        setPosition = transform.position;
        risePosition = transform.position + new Vector3(0, 5, 0);
        spornPosition = transform.parent.position;

        var downSet = UnityEngine.Random.Range(2, 8) + UnityEngine.Random.value;
        
        transform.parent.transform.position -= new Vector3(0, downSet, 0);
        sporning = true;
    }
}

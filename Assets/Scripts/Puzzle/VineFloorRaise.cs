using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineFloorRaise : MonoBehaviour
{
    public GameObject vineFloor, newHeightDangers, previousHeightDangers;
    public float riseHeight, risingSpeed = 1;

    public BoxCollider[] triggers;
    Vector3 risePosition;
    bool isRising;
    private void Start()
    {
        risePosition = vineFloor.transform.position + new Vector3(0, riseHeight, 0);
        triggers = GetComponentsInChildren<BoxCollider>();
        //Debug.Log(triggers);
    }
    public void RaiseFloor()
    {
        isRising = true;
        //gameObject.GetComponent<Collider>().enabled = false;
        foreach (BoxCollider col in triggers)
        {
            col.enabled = false;
        }
    }
    private void Update()
    {
        if (isRising)
        {
            if (risePosition.y - vineFloor.transform.position.y > 0.1)
            {
                vineFloor.transform.position = Vector3.Lerp(vineFloor.transform.position, risePosition, risingSpeed * Time.deltaTime);
            }
            else
            {
                isRising = false;
                vineFloor.transform.position = risePosition;
                if (previousHeightDangers != null)
                    previousHeightDangers.SetActive(false);
                if (newHeightDangers != null)
                    newHeightDangers.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}

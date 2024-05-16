using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineFloorRaise : MonoBehaviour
{
    public GameObject vineFloor, newHeightDangers, previousHeightDangers;
    public float riseHeight, risingSpeed = 1;

    Vector3 risePosition;
    bool isRising;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            isRising = true;
            risePosition = vineFloor.transform.position + new Vector3(0, riseHeight, 0);
            this.gameObject.GetComponent<Collider>().enabled = false;
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
                previousHeightDangers.SetActive(false);
                newHeightDangers.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}

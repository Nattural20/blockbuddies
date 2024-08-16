using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    int waterFill = 3;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            if (waterFill != 0)
            {
                Destroy(collision.gameObject);
                waterFill--;
            }
        }
        else if (collision.gameObject.CompareTag("WaterSource"))
        {
            waterFill = 3;
        }
    }
}

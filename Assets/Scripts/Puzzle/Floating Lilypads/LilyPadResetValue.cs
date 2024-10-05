using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadResetValue : MonoBehaviour
{
    public float resetDistance;
    public void LeftoverSpocks(GameObject leftovers)
    {
        StartCoroutine(DestroyDelay(leftovers));
    }
    IEnumerator DestroyDelay(GameObject spocks)
    {
        yield return new WaitForSeconds(3);
        Destroy(spocks);
    }
}

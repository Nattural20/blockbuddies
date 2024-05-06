using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSolve : MonoBehaviour
{
    public int runesSolved = 0;

    public float riseHeight;
    public float riseSpeed;
    bool activated = false;

    Vector3 hightAim;

    private void Start()
    {
        hightAim = transform.position + new Vector3 (0, 0, riseHeight);
    }
    private void Update()
    {
        if (!activated)
        {
            if (runesSolved == 3)
            {
                StartCoroutine(MoveGate());
                activated = true;
            }
        }
    }
    private IEnumerator MoveGate()
    {
        while (transform.position != hightAim)
        {
            transform.position = Vector3.Lerp(transform.position, hightAim, riseSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

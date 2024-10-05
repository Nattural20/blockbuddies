using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndViewTrigger : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject freeLookCam, endViewCam;
    public Image endScreen;
    public float endScreenFadeSpeed;
    bool isFinished = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            freeLookCam.SetActive(false);
            endViewCam.SetActive(true);

            endScreen.gameObject.SetActive(true);
            isFinished = true;
        }
    }
    private void Update()
    {
        if (isFinished)
        {
            endScreen.color = new Color(endScreen.color.r, endScreen.color.g, endScreen.color.b, endScreen.color.a + endScreenFadeSpeed * Time.deltaTime);
        }
    }
}

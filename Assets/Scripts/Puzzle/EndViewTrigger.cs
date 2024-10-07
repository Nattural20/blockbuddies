using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndViewTrigger : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject freeLookCam, endViewCam, rotator, rotationCam;
    public Image endScreen;
    public float endScreenFadeSpeed;
    bool isFinished = false;
    public float speed;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            freeLookCam.SetActive(false);
            endViewCam.SetActive(true);

            endScreen.gameObject.SetActive(true);
            isFinished = true;
            StartCoroutine(CameraRotation());
        }

        if (isFinished == true)
        {
            
        }
    }
    private void Update()
    {
        if (isFinished)
        {
            endScreen.color = new Color(endScreen.color.r, endScreen.color.g, endScreen.color.b, endScreen.color.a + endScreenFadeSpeed * Time.deltaTime);
        }
    }

    private IEnumerator CameraRotation()
    {
        yield return new WaitForSeconds(2f * Time.deltaTime);
        endViewCam.SetActive(false);
        rotator.SetActive(true);
        rotationCam.SetActive(true);
    }
}

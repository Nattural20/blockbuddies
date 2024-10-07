using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndViewTrigger : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject freeLookCam, endViewCam, rotator, rotationCam;
    public Image endScreen;
    public float endScreenFadeSpeed;
    bool isFinished = false;
    public float rotationDelay;
    public int endResetTimer;
    public bool unlimited;
    public bool reset = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            freeLookCam.SetActive(false);
            endViewCam.SetActive(true);

            endScreen.gameObject.SetActive(true);
            isFinished = true;
            StartCoroutine(CameraRotation());
            unlimited = true;
            if (rotationCam == enabled)
            {
                StartCoroutine(EndResetCountdown());
            }

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
        yield return new WaitForSecondsRealtime(rotationDelay);
        endViewCam.SetActive(false);
        rotator.SetActive(true);
        rotationCam.SetActive(true);
    }

    private IEnumerator EndResetCountdown()
    {
        yield return new WaitForSecondsRealtime(endResetTimer);
        reset = true;
    }
}

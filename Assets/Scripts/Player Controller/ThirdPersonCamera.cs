using UnityEngine;
using Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform playerTransform;
    public CinemachineFreeLook freeLookCamera;
    public float rotationSpeed = 1000f;
    public string horizontalAxisName = "RightStickHorizontal";
    public string verticalAxisName = "RightStickVertical";

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned to the ThirdPersonCamera script!");
            return;
        }

        if (freeLookCamera == null)
        {
            Debug.LogError("Cinemachine FreeLook Camera not assigned to the ThirdPersonCamera script!");
            return;
        }

        // Set the player transform as the follow target for the Cinemachine FreeLook camera
        //freeLookCamera.Follow = playerTransform;
    }

    void Update()
    {
        // Get input for rotating the camera horizontally and vertically using PS4 controller's right analog stick
        float horizontalInput = Input.GetAxis(horizontalAxisName);
        float verticalInput = Input.GetAxis(verticalAxisName);

        // Rotate the camera around the player based on input
        freeLookCamera.m_XAxis.Value += horizontalInput * rotationSpeed * Time.deltaTime;
        freeLookCamera.m_YAxis.Value += verticalInput * rotationSpeed * Time.deltaTime;
    }
}

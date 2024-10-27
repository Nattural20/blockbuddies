using UnityEngine;

public class CubertMovement : MonoBehaviour
{
    public Transform CubertAimPosition;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    void FixedUpdate()
    {
        if (CubertAimPosition != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, CubertAimPosition.position, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            Quaternion targetRotation = CubertAimPosition.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.z = 0;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}

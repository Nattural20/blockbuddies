using UnityEngine;

public class CubertMovement : MonoBehaviour
{
    public Transform CubertAimPosition;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (CubertAimPosition != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, CubertAimPosition.position, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            Quaternion targetRotation = CubertAimPosition.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

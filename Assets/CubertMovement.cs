using UnityEngine;

public class CubertMovement : MonoBehaviour
{
    public bool cubertOnLock;
    public Vector3 currentSpawnLockPosition;
    public GameObject AimPosParent;
    public Transform playerTransform;

    public Transform CubertAimPosition;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    public Animator movementAnim;

    void FixedUpdate()
    {
        if (CubertAimPosition != null)
        {
            Vector3 direction = (CubertAimPosition.position - transform.position).normalized;
            Vector3 newPosition = Vector3.Lerp(transform.position, CubertAimPosition.position, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        if (cubertOnLock)
        {
            AimPosParent.transform.position = currentSpawnLockPosition;
            movementAnim.SetBool("isMoving", false);
        }
        else
        {
            AimPosParent.transform.position = playerTransform.position;
            movementAnim.SetBool("isMoving", true);
        }

    }
}

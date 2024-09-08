using UnityEngine;

public class GhostSpocksController : MonoBehaviour
{
    public Transform playerCamera;
    public float distanceFromPlayer = 5f;
    public float distanceAwayFromWall = 0.1f;
    public Color rayColor = Color.red;
    public LayerMask ignoreLayer;

    void Update()
    {
        Vector3 desiredLocalPosition = Vector3.forward * distanceFromPlayer;

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceFromPlayer, ~ignoreLayer);

        Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceFromPlayer, rayColor);

        if (hitSomething)
        {
            Collider hitCollider = hit.collider;

            if (!hitCollider.isTrigger)
            {
                float adjustedDistance = hit.distance - distanceAwayFromWall;
                transform.localPosition = Vector3.forward * Mathf.Max(adjustedDistance, 0);
            }
            else
            {
                transform.localPosition = desiredLocalPosition;
            }
        }
        else
        {
            transform.localPosition = desiredLocalPosition;
        }
    }
}

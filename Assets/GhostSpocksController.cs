using UnityEngine;

public class GhostSpocksController : MonoBehaviour
{
    public Transform playerCamera; // Reference to the camera
    public float distanceFromPlayer = 5f; // Desired distance in front of the player
    public Color rayColor = Color.red; // Color of the debug ray

    void Update()
    {
        // Calculate the desired local position of Ghost Spocks
        Vector3 desiredLocalPosition = Vector3.forward * distanceFromPlayer;

        // Cast a ray from the camera towards the desired position
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceFromPlayer);

        // Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceFromPlayer, rayColor);

        if (hitSomething)
        {
            Collider hitCollider = hit.collider;

            // Check if the hit object has a non-trigger collider
            if (!hitCollider.isTrigger)
            {
                // If there's an obstacle, adjust Ghost Spocks' local position to be just before the obstacle
                float adjustedDistance = hit.distance - 0.1f; // Offset to prevent clipping
                transform.localPosition = Vector3.forward * adjustedDistance;
            }
            else
            {
                // No valid obstacle, move Ghost Spocks to the desired local position
                transform.localPosition = desiredLocalPosition;
            }
        }
        else
        {
            // No obstacle, move Ghost Spocks to the desired local position
            transform.localPosition = desiredLocalPosition;
        }

        // Optionally, make Ghost Spocks always face the player or the camera direction
        // transform.LookAt(playerCamera);
    }
}

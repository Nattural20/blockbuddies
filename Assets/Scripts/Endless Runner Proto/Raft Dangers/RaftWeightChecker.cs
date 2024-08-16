using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftWeightChecker : MonoBehaviour
{
    public RaftMovement raft;
    public bool addWeight;

    private void Start()
    {
        raft = GetComponentInParent<RaftMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WeightObstacle"))
        {
            if (addWeight)
            {
                raft.obstacleWeightAdd++;
            }
            else
            {
                raft.obstacleWeightAdd--;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WeightObstacle"))
        {
            if (addWeight)
            {
                raft.obstacleWeightAdd--;
            }
            else
            {
                raft.obstacleWeightAdd++;
            }
        }
    }
}

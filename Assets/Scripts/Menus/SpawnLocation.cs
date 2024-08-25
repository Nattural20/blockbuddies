using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnLocation", menuName = "ScriptableObjects/SpawnLocation", order = 1)]
public class SpawnLocation : ScriptableObject
{
    public string locationName;
    public Vector3 spawnLocation;
}

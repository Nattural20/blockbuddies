using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRotationManager : MonoBehaviour
{
    public enum Orientation
    {
        Left,
        Up,
        Right,
        Down
    }

    private static SpawnerRotationManager instance;

    public static SpawnerRotationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnerRotationManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SpawnerRotationManager");
                    instance = go.AddComponent<SpawnerRotationManager>();
                }
            }
            return instance;
        }
    }

    public SpawnerRotationManager.Orientation CurrentOrientation { get; set; } = Orientation.Left;

    // Method to set orientation from dropdown
    public void SetOrientation(Orientation newOrientation)
    {
        CurrentOrientation = newOrientation;
    }
}

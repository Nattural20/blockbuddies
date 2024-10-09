using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.Playables;

public class VineDoorAnimTest : MonoBehaviour
{
    public AlembicStreamPlayer player;

    Collider[] colliders;

    bool open;
    float time;
    private void Start()
    {
        colliders = GetComponents<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            open = true;
            foreach (Collider collider in colliders)
            {
                Destroy(collider);
            }
        }
    }
    void Update()
    {
        if (open)
        {
            time += Time.deltaTime;
            if (time > 2.5)
            {
                Destroy(this);
            }
            player.CurrentTime = time;
        }
    }
}

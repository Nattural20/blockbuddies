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
            if (player.CurrentTime < player.EndTime)
            {
                Destroy(this);
            }
            player.CurrentTime += Time.deltaTime;
        }
    }
}

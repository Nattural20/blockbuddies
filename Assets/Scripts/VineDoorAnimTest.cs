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
    float start_cut_frame = 4;
    float end_cut_frame = 20;
    float total_frames = 60;
    float start_cut_time;
    float end_cut_time;

    private void Start()
    {
        colliders = GetComponents<Collider>();
        start_cut_time = start_cut_frame / total_frames;
        end_cut_time = end_cut_frame / total_frames;
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

            if (time > start_cut_time && time < end_cut_time) time = end_cut_time * 2.5f;

            if (time > 2.5)
            {
                Destroy(this);
            }
            player.CurrentTime = time;
        }
    }
}

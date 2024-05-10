using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;
    public AudioListener musicListener;
    public AudioClip musicClip;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play(0);

        music.clip = musicClip;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        music.loop = true;
    }
}

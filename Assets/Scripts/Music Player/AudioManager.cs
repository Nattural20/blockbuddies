using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

    }

    private void Start()
    {
        Play("ThemeNeutral");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found.");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found.");
            return;
        }
        s.source.Stop();
    }

    /// <summary>
    ///Fades the first sound out and the second sound in over the duration of time provided. 
    /// </summary>
    public void CrossFade(string fadeOut, string fadeIn, float duration)
    
    {
        Sound soundOut = Array.Find(sounds, sound => sound.name == fadeOut);
        Sound soundIn = Array.Find(sounds, sound => sound.name == fadeIn);

        if (soundOut == null)
        {
            Debug.Log("Sound " + fadeOut + " not found.");
            return;
        }
        if (soundIn == null)
        {
            Debug.Log("Sound " + fadeIn + " not found.");
            return;
        }

        //check that the sound is even playing man...
        if (!soundOut.source.isPlaying)
        {
            Debug.Log(fadeOut + " isn't playing... fading in only.");
            StartCoroutine(FadeIn(soundIn, duration));
            return;
        }

        //Start fading out the current sound and fading in the new one
        StartCoroutine(FadeOutIn(soundOut, soundIn, duration));
    }

    private IEnumerator FadeOutIn(Sound soundOut, Sound soundIn, float duration)
    {
        float time = 0f;
        float startVolumeOut = soundOut.source.volume;
        float startVolumeIn = soundIn.source.volume;

        soundIn.source.Play();
        soundIn.source.volume = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            soundOut.source.volume = Mathf.Lerp(startVolumeOut, 0f, t);
            soundIn.source.volume = Mathf.Lerp(0f, startVolumeIn, t);

            yield return null;
        }

        soundOut.source.Stop(); // Stop the fadeOut sound once it's done
        soundOut.source.volume = startVolumeOut; // Reset its volume in case it's used again
    }

    private IEnumerator FadeIn(Sound soundIn, float duration)
    {
        float time = 0f;
        soundIn.source.Play();
        soundIn.source.volume = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            soundIn.source.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }
    }

}

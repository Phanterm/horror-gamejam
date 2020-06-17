using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public string currentBGM;

    void Awake ()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //Play("BGM");
    }

    /*USAGE
     * 0. Add the appropriate sounds in the AudioManager object.
     * 1. Go to any script from which you want to trigger a sound.
     * 2. Where you wish to call the sound, type FindObjectOfType<AudioManager>().Play("yourSoundNameHere");
     * 
     */
    public void Play(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
    }

    public void Stop(string name)
    { 
         Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (s.isBGM)
            {
                s.source.Stop();
            }
        }
    }

    public void ChangeBGM(string songName)
    {
        foreach (Sound s in sounds)
        {
            if(s.isBGM)
            {
                s.source.Stop();
            }

            Play(songName);
            currentBGM = songName;
        }
    }

    public IEnumerator FadeOut(string name, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        float startVolume = s.source.volume;
        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        s.source.Stop();
    }

    public IEnumerator FadeIn(string name, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Play();
        s.source.volume = 0f;
        while (s.source.volume < 1)
        {
            s.source.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}

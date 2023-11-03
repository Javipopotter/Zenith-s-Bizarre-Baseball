using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake() {
        instance = this;
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        } 
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound._name == name);
        if(s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.OnPlay();
        s.source.Play();
    }
}

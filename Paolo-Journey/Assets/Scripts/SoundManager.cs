using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private Sound[] sounds;

    // List of sounds
    public enum SoundName
    {
        ThemeSong,
    }

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        {
            Destroy(this);
        }
    }

    // For setting sound
    public void Play(SoundName soundName)
    {
        var sound = GetSound(soundName);
        if (sound.audioSource == null)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
        }
        sound.audioSource.Play();
    }

    private Sound GetSound(SoundName soundName)
    {
        return Array.Find(sounds, s => s.soundName == soundName);
    }
}
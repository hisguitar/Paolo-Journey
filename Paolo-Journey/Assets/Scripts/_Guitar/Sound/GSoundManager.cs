using System;
using UnityEngine;

public class GSoundManager : SingletonPersistent<GSoundManager>
{
	[SerializeField] private GSound[] sounds;
	private GSound currentMusic;
	
	// List of sounds
	public enum GSoundName
	{
		ThemeSong,
		TrueOrFalse,
		Correct,
		Wrong,
		ClickButton
	}

	// For setting sound
	public void Play(GSoundName soundName)
	{
		var sound = GetSound(soundName);
		
		if (sound.audioMixerGroup.name == "Music")
		{
			if (currentMusic == sound && sound.audioSource.isPlaying) return;
			if (currentMusic != null && currentMusic.audioSource != null)
			{
				currentMusic.audioSource.Stop();
			}
			currentMusic = sound;
		}
		if (sound.audioSource == null)
		{
			sound.audioSource = gameObject.AddComponent<AudioSource>();
			sound.audioSource.clip = sound.clip;
			sound.audioSource.volume = sound.volume;
			sound.audioSource.loop = sound.loop;
			sound.audioSource.outputAudioMixerGroup = sound.audioMixerGroup;
		}
		sound.audioSource.Play();
	}

	private GSound GetSound(GSoundName soundName)
	{
		return Array.Find(sounds, s => s.soundName == soundName);
	}
	
	public void Stop(GSoundName soundName)
	{
		var sound = GetSound(soundName);
		if (sound.audioSource != null)
		{
			sound.audioSource.Stop();
		}
	}
	
	public void StopAll()
	{
		foreach (var sound in sounds)
		{
			if (sound.audioSource != null)
			{
				sound.audioSource.Stop();
			}
		}
	}
}
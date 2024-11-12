using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class GSound
{
	public GSoundManager.GSoundName soundName;
	public AudioClip clip;
	public AudioMixerGroup audioMixerGroup;
	public bool loop;
	[Range(0f, 1f)] public float volume;

	[HideInInspector] public AudioSource audioSource;
}
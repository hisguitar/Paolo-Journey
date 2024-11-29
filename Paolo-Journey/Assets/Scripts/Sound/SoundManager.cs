using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField] private Sound[] sounds;
	[SerializeField] private AudioMixerGroup sfxGroup; // เพิ่ม AudioMixerGroup สำหรับ SFX
	private static SoundManager instance;
	public AudioClip defaultMusic; // เพลงสำหรับ Scene ปกติ
	public AudioClip iqTestMusic;  // เพลงสำหรับ Scene IQ Test
	public AudioClip virusMusic;  // เพลงสำหรับ Scene IQ Test

	private AudioSource audioSource;

	private void Start()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = defaultMusic; // ตั้งค่าเพลงเริ่มต้น
			audioSource.Play();

			// ลงทะเบียนฟังก์ชันเมื่อ Scene โหลดเสร็จ
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		
		
		if (scene.name == "IQTestMenu" || scene.name == "17+" || scene.name == "Result")
		{
			if (audioSource.clip != iqTestMusic)
			{
				audioSource.clip = iqTestMusic;
				audioSource.Play();
			}
		}
		else if(scene.name == "NewGame2" || scene.name == "NewGame2Menu")
		{
			if (audioSource.clip != virusMusic)
			{
				audioSource.clip = virusMusic;
				audioSource.Play();
			}
		}
		// เช็คชื่อ Scene และเปลี่ยนเพลงตามต้องการ
		else if (scene.name == "TrueOrFalse")
		{
			audioSource.Stop();
		}
		else if(scene.name == "Menu" || scene.name == "PaoloJourney" || scene.name == "Guide")
		{
			if (audioSource.clip != defaultMusic)
			{
				audioSource.clip = defaultMusic;
				audioSource.Play();
			}
		}
		else
		{
			if (audioSource.clip != defaultMusic)
			{
				audioSource.clip = defaultMusic;
				audioSource.Play();
			}
		}
		
	}

	private void OnDestroy()
	{
		// ยกเลิกการลงทะเบียนเมื่อ GameObject ถูกทำลาย
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	// List of sounds
	public enum SoundName
	{
		ThemeSong,
		Click,
		Correct,
		Wrong,
		ClickButton1,
		ClickButton2,
		True,
		False
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
			
			sound.audioSource.outputAudioMixerGroup = sfxGroup;
		}
		sound.audioSource.Play();
	}

	private Sound GetSound(SoundName soundName)
	{
		return Array.Find(sounds, s => s.soundName == soundName);
	}
}

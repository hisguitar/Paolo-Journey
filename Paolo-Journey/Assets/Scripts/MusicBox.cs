using UnityEngine;

public class MusicBox : MonoBehaviour
{
	/*YOU HAVE TO CREATE NEW OBJECT IN HIERARCHY AND PUT THIS SCRIPT IN TO IT
	 AND PUT THAT OBJECT TO BE PREFAB, SO YOU CAN USE IT IN EVERY SCENE*/
	private enum MusicName
	{
		ThemeSong,
		TrueOrFalse,
	}
	
	[SerializeField] private MusicName musicName; // Type name of Background Music

	// Update is called once per frame
	public void Start()
	{	
		switch (musicName)
		{
			// Start new music
			case MusicName.ThemeSong:
				SoundManager.Instance.Play(SoundManager.SoundName.ThemeSong);
				break;
			case MusicName.TrueOrFalse:
				SoundManager.Instance.Play(SoundManager.SoundName.TrueOrFalse);
				break;
		}
	}
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}

	public void To_PaoloJourney()
	{
		SceneManager.LoadScene("PaoloJourney");
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}

	public void To_GameplayManager()
	{
		SceneManager.LoadScene("GameplayManager");
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}
	
	#region Media
	public void To_Media1()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Media1");
	}

	public void To_Media2()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Media2");
	}

	public void To_Media3()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Media3");
	}
	
	public void To_ChooseTheCorrectWord()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("ChooseTheCorrectWord");
	}

	public void To_IQTestMenu()
	{
		SceneManager.LoadScene("IQTestMenu");
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}
	#endregion
	#region Game
	public void To_Game1()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Game1");
	}
	
	public void To_Game2()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Game2");
	}

	public void ButtonGame3()
	{
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene("Game3");
	}
	#endregion
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class GSceneChanger : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}
}
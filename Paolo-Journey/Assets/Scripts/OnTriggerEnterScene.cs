using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerEnterScene : MonoBehaviour
{
	[SerializeField] private string sceneName;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(sceneName);
			SoundManager.Instance.Play(SoundManager.SoundName.Click);
		}
	}
}
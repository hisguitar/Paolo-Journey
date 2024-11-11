using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerEnterScene : MonoBehaviour
{
	[SerializeField] private GameObject interactButton;
	[SerializeField] private string sceneName;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			interactButton.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			interactButton.SetActive(false);
		}
	}
	
	public void ChangeScene()
	{
		SceneManager.LoadScene(sceneName);
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
	}
}
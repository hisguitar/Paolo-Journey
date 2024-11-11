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
			if (interactButton != null)
			{
				interactButton.SetActive(false);
			}
		}
	}
	
	public void ChangeScene()
	{
		interactButton.SetActive(false);
		
		SoundManager.Instance.Play(SoundManager.SoundName.Click);
		SceneManager.LoadScene(sceneName);
	}
}
using UnityEngine;

public class PauseResume : MonoBehaviour
{
	[SerializeField] private GameObject pauseButton;
	[SerializeField] private GameObject resumeButton;
	
	public void PauseGame()
	{
		pauseButton.SetActive(false);
		resumeButton.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void ResumeGame()
	{
		pauseButton.SetActive(true);
		resumeButton.SetActive(false);
		Time.timeScale = 1;
	}
}
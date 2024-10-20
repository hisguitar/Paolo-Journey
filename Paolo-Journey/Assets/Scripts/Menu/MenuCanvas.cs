using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
	public GameObject player;
	public GameObject playerUI;
	public GameObject pauseButton;
	public GameObject menu;
	public GameObject blackBackground;
	
	public Button playButton;
	public Button iqTestButton;
	public Button optionsButton;
	public Button quitButton;
	public static bool isLevelComplete = false;
	void Start()
	{
		if (isLevelComplete) // เช็คว่าเล่นด่านเสร็จหรือยัง
		{
			player.SetActive(true);
			playerUI.SetActive(true);
			pauseButton.SetActive(true);
			menu.SetActive(false);
			blackBackground.SetActive(false);
		}
		else
		{
			player.SetActive(false);
			playerUI.SetActive(false);
			pauseButton.SetActive(false);
			menu.SetActive(true);
			blackBackground.SetActive(true);
		}
	}

	public void PlayButton()
	{
		player.SetActive(true);
		playerUI.SetActive(true);
		pauseButton.SetActive(true);
		menu.SetActive(false);
		blackBackground.SetActive(false);
	}
	
	public void PauseButton()
	{
		player.SetActive(false);
		playerUI.SetActive(false);
		pauseButton.SetActive(false);
		menu.SetActive(true);
		blackBackground.SetActive(true);
	}
}

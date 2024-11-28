using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionSaver : MonoBehaviour
{
	public PlayerData playerData;
	public GameObject player;
	public string sceneName = "PaoloJourney";

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == sceneName && playerData.hasSavedPosition)
		{
			player.transform.position = playerData.lastPosition;
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "PaoloJourney" && playerData.hasSavedPosition)
		{
			player.transform.position = playerData.lastPosition;
		}
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void SavePosition()
	{
		if (SceneManager.GetActiveScene().name == "PaoloJourney")
		{
			playerData.lastPosition = player.transform.position;
			playerData.hasSavedPosition = true;
		}
	}
}
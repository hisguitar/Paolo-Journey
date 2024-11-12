using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : SingletonPersistent<FadeTransition>
{
	[SerializeField] private Image fadeImage;
	[SerializeField] private float fadeDuration = 1.0f;

	private void Start()
	{
		fadeImage.gameObject.SetActive(true);
		StartCoroutine(FadeIn());
	}

	private void OnEnable()
	{
		// Register callback for scene load
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		// Unregister callback
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Start Fade In when new scene is loaded
		StartCoroutine(FadeIn());
	}

    public void FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeOut(sceneName));
    }

	private IEnumerator FadeIn()
	{
		fadeImage.gameObject.SetActive(true);
		Color color = fadeImage.color;
		color.a = 1;
		fadeImage.color = color;

		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
			fadeImage.color = color;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		color.a = 0;
		fadeImage.color = color;
		fadeImage.gameObject.SetActive(false); // Disable after fade-in is complete
	}

	private IEnumerator FadeOut(string sceneName)
	{
		Color color = fadeImage.color;
		color.a = 0;
		fadeImage.color = color;

		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
			fadeImage.color = color;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		color.a = 1;
		fadeImage.color = color;

		SceneManager.LoadScene(sceneName); // Load the new scene after fade-out is complete
	}
}

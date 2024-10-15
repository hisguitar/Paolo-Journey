using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TriggerName
{
	isGame1Cleared,
	isGame2Cleared,
}

public class TriggerMedia : MonoBehaviour
{
	[SerializeField] private TriggerState triggerState;
	[SerializeField] private TriggerName triggerName;
	[SerializeField] private string sceneName;
	
	private void Start()
	{
		switch (triggerName)
		{
			case TriggerName.isGame1Cleared:
			if (triggerState.isGame1Cleared)
			{
				gameObject.SetActive(false);
			}
			break;
			
			case TriggerName.isGame2Cleared:
			if (triggerState.isGame2Cleared)
			{
				gameObject.SetActive(false);
			}
			break;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(sceneName);
			SoundManager.Instance.Play(SoundManager.SoundName.Click);
		}
	}
}
using UnityEngine;

public class PhymonGuideTrigger : MonoBehaviour
{
	[Header("Chat Bubble")]
	[SerializeField] private GameObject chatBox;
	[SerializeField] private GameObject interactButton;
	[SerializeField] private GameObject background;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			background.SetActive(true);
			chatBox.SetActive(true);
			interactButton.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			background.SetActive(false);
			chatBox.SetActive(false);
			interactButton.SetActive(false);
		}
	}
}
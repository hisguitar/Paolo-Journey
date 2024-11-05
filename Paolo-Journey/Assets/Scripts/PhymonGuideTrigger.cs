
using UnityEngine;

public class PhymonGuideTrigger : MonoBehaviour
{
	[Header("Chat Bubble")]
	[SerializeField] private GameObject chatBox;
	[SerializeField] private GameObject interactButton;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			chatBox.SetActive(true);
			interactButton.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			chatBox.SetActive(false);
			interactButton.SetActive(false);
		}
	}
}
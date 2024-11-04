
using UnityEngine;

public class PhymonGuideTrigger : MonoBehaviour
{
	[Header("Chat Bubble")]
	[SerializeField] private GameObject chatBubble;
	[SerializeField] private GameObject interactButton;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			chatBubble.SetActive(true);
			interactButton.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			chatBubble.SetActive(false);
			interactButton.SetActive(false);
		}
	}
}
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
	[SerializeField] private TMP_Text messageText;
	[SerializeField] private GameObject noteButton;
	
	[SerializeField] [TextArea] private string[] messages;
	private int currentMessageIndex = 0;
	
	// Function for interaction
	public void ChangeText()
	{
		if (messages.Length == 0 || currentMessageIndex == messages.Length - 1) return;
		
		// Change to next message
		currentMessageIndex = (currentMessageIndex + 1) % messages.Length;
		
		// Show message
		messageText.text = messages[currentMessageIndex];
		
		// Show note button
		if (currentMessageIndex == messages.Length - 1)
		{
			noteButton.SetActive(true);
		}
	}
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
	[SerializeField] private Image background;
	[SerializeField] private TMP_Text messageText;
	[SerializeField] private Vector2 bubbleSizeOffset = new(0.4f, 0.2f);
	
	[SerializeField] [TextArea] private string[] messages;
	private int currentMessageIndex = 0;
	
	private void OnEnable()
	{
		UpdateBackgroundSize();
	}
	
	public void SetText(string text)
	{
		messageText.text = text;
		UpdateBackgroundSize();
	}
	
	private void UpdateBackgroundSize()
	{
		// Force update the text mesh to ensure accurate size calculation
		messageText.ForceMeshUpdate();
		
		// Get width and height of the rendered text
		float textWidth = messageText.renderedWidth;
		float textHeight = messageText.renderedHeight;
		
		// Set size of background to match textWidth & textHeight
		// Update background size, converting to the appropriate RectTransform size
		RectTransform backgroundRect = background.GetComponent<RectTransform>();
		backgroundRect.sizeDelta = new Vector2(textWidth, textHeight) + bubbleSizeOffset; // ใช้ sizeDelta สำหรับ RectTransform
	}
	
	// Function for interaction
	public void ChangeText()
	{
		if (messages.Length == 0) return;
		
		// Show message
		messageText.text = messages[currentMessageIndex];
		
		// Change to next message
		currentMessageIndex = (currentMessageIndex + 1) % messages.Length;
		UpdateBackgroundSize();
	}
}
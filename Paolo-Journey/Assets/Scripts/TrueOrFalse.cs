using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrueOrFalse : MonoBehaviour
{
	// 1 proposition : 2 choices
	[SerializeField] private TMP_Text finishScoreText;
	[SerializeField] private Image proposition;
	[SerializeField] private Button choiceOne;
	[SerializeField] private Button choiceTwo;
	[SerializeField] private Sprite[] propositionImage;
	[SerializeField] private Sprite[] choiceOneImage;
	[SerializeField] private Sprite[] choiceTwoImage;
	[SerializeField] private int finished = 0;
	[SerializeField] private int score = 0;
	[SerializeField] private int currentProposition = 0;
	private RectTransform choiceOneTransform;
	private RectTransform choiceTwoTransform;
	
	private void Start()
	{
		SetImage();
	}
	
	public void ChangeProposition()
	{
		currentProposition++;
		UpdateScore();
		SetImage();
	}
	
	public void TrueChoice()
	{
		score += 10;
	}
	
	private void UpdateScore()
	{
		finishScoreText.text = finished + "/" + (propositionImage.Length - 1) + " Finished\n" +
		score + " Score";
	}
	
	private void SetImage()
	{
		proposition.sprite = propositionImage[currentProposition];
		choiceOne.image.sprite = choiceOneImage[currentProposition];
		choiceTwo.image.sprite = choiceTwoImage[currentProposition];
	}
}
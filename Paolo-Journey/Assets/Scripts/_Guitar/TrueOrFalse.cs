using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrueOrFalse : MonoBehaviour
{
	// 1 proposition : 2 choices
	[Header("Question Elements")]
	[SerializeField] private Image question;
	//[SerializeField] private Button choiceOne;
	//[SerializeField] private Button choiceTwo;
	[SerializeField] private TMP_Text imageNameText;
	[SerializeField] private GameObject foodDescriptionBackground;
	[SerializeField] private TMP_Text foodDescriptionText;
	[TextArea] [SerializeField] private string[] foodDescription;
	[Header("Result")]
	[SerializeField] private Button correctAnswer;
	[SerializeField] private Image correctAnswerBanner;
	[SerializeField] private TMP_Text correctAnswerText;
	[SerializeField] private TMP_Text finishScoreText;
	[Header("Image List")]
	[Tooltip("questionImage, choiceOneImage, choiceTwoImage must be images that are in the same question.")] [SerializeField] private Sprite[] questionImage;
	//[Tooltip("questionImage, choiceOneImage, choiceTwoImage must be images that are in the same question.")] [SerializeField] private Sprite[] choiceOneImage;
	//[Tooltip("questionImage, choiceOneImage, choiceTwoImage must be images that are in the same question.")] [SerializeField] private Sprite[] choiceTwoImage;
	[SerializeField] private string[] imageName;
	[SerializeField] private bool[] isShouldEat;
	[SerializeField] private int score = 0;
	[SerializeField] private int currentQuestion = 0;
	[SerializeField] private string sceneName;
	private bool isEnd = false;
	//private Vector3 choiceOnePosition;
	//private Vector3 choiceTwoPosition;
	
	private void Start()
	{
		correctAnswer.onClick.AddListener(Hide);
		UpdateScore();
		SetImage();
		
		// Save anchored position of button for random in future
		//choiceOnePosition = choiceOne.GetComponent<RectTransform>().anchoredPosition;
		//choiceTwoPosition = choiceTwo.GetComponent<RectTransform>().anchoredPosition;
	}
	
	public void ChangeQuestion()
	{
		if (currentQuestion < questionImage.Length)
		{
			currentQuestion++;
		}
		UpdateScore();
	}
	
	/// <summary>
	/// Put only in true choice,
	/// in position of choice, i will use other methods to manage.
	/// </summary>
	public void TrueOrNot(bool isTrue)
	{
		correctAnswer.gameObject.SetActive(true);
		StartCoroutine(ExpandBannerHeight(0, 250, 0.4f));
		
		if (currentQuestion >= questionImage.Length) return;
		if (isTrue && isShouldEat[currentQuestion] || !isTrue && !isShouldEat[currentQuestion])
		{	
			GSoundManager.Instance.Play(GSoundManager.GSoundName.Correct);
			score += 10;
			correctAnswer.image.color = new Color32(113, 169, 0, 210);
			correctAnswerText.color = new Color32(171, 255, 0, 255);
			correctAnswerText.text = "Correct Answer!\n+10 point";
		}
		else
		{
			GSoundManager.Instance.Play(GSoundManager.GSoundName.Wrong);
			correctAnswer.image.color = new Color32(168, 2, 0, 210);
			correctAnswerText.color = new Color32(255, 0, 0, 255);
			correctAnswerText.text = "Wrong Answer!\nno point";
		}
	}
	
	private IEnumerator ExpandBannerHeight(float startHeight, float endHeight, float duration)
	{
		float elapsed = 0f;
		RectTransform rt = correctAnswerBanner.GetComponent<RectTransform>();
		
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float newHeight = Mathf.Lerp(startHeight, endHeight, elapsed / duration);
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, newHeight);
			yield return null;
		}
		
		// Ensure final height is set
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, endHeight);
	}
	
	private void Hide()
	{
		if (isEnd)
		{
			Debug.Log("Load next scene here!");
			SceneManager.LoadScene(sceneName);
		}
		
		if (currentQuestion >= questionImage.Length)
		{
			// Complete here!
			Debug.Log("There is no more question.");
			correctAnswer.image.color = new Color32(0, 163, 255, 210);
			correctAnswerText.color = Color.white;
			correctAnswerText.text = $"You have answered all questions.\nYour score is {score}/{questionImage.Length * 10}";
			isEnd = true;
			return;
		}
		else
		{
			correctAnswer.gameObject.SetActive(false);
		
			// ChangeQuestion
			SetImage();
			//RandomizeChoicesPosition();
		}
	}
	
	#region Used in ChangeQuestion()
	// Used in ChangeQuestion()
	private void UpdateScore()
	{
		finishScoreText.text = currentQuestion + "/" + questionImage.Length + " Finished\n" +
		score + "/" + questionImage.Length * 10 + " Score";
		StartCoroutine(PopupText(1.0f, 1.15f, 0.25f));
	}
	
	// Used in ChangeQuestion()
	private void SetImage()
	{
		// Set image sprite to [currentProposition] number in array list
		question.sprite = questionImage[currentQuestion];
		//choiceOne.image.sprite = choiceOneImage[currentQuestion];
		//choiceTwo.image.sprite = choiceTwoImage[currentQuestion];
		imageNameText.text = imageName[currentQuestion];
	}
	
	// Used in ChangeQuestion()
	// private void RandomizeChoicesPosition()
	// {
	// 	bool shouldSwap = Random.Range(0, 2) == 1; // 50% chances because it doesn't include the last number, which is 2.
		
	// 	if (shouldSwap)
	// 	{
	// 		choiceOne.GetComponent<RectTransform>().anchoredPosition = choiceTwoPosition;
	// 		choiceTwo.GetComponent<RectTransform>().anchoredPosition = choiceOnePosition;
	// 	}
	// 	else
	// 	{
	// 		choiceOne.GetComponent<RectTransform>().anchoredPosition = choiceOnePosition;
	// 		choiceTwo.GetComponent<RectTransform>().anchoredPosition = choiceTwoPosition;
	// 	}
	// }
	#endregion
	
	private IEnumerator PopupText(float startScale, float endScale, float duration)
	{
		float elapsedTime = 0f;

		// Pop-out
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float scale = Mathf.Lerp(startScale, endScale, elapsedTime / duration);
			finishScoreText.rectTransform.localScale = new Vector3(scale, scale, 1f);
			yield return null;
		}

		// Pop-in
		elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float scale = Mathf.Lerp(endScale, startScale, elapsedTime / duration);
			finishScoreText.rectTransform.localScale = new Vector3(scale, scale, 1f);
			yield return null;
		}

		finishScoreText.rectTransform.localScale = new Vector3(startScale, startScale, 1f);
	}
	
	public void OpenFoodDescription(bool isOpen)
	{
		if (isOpen)
		{
			GSoundManager.Instance.Play(GSoundManager.GSoundName.ClickButton);
			foodDescriptionBackground.SetActive(true);
			if (currentQuestion >= questionImage.Length) return;
			foodDescriptionText.text = foodDescription[currentQuestion];
		}
		else
		{
			foodDescriptionBackground.SetActive(false);
		}
	}
}
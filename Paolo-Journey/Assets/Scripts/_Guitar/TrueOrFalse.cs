using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrueOrFalse : MonoBehaviour
{
	[Header("Question Elements")]
	public Image question;
	public TMP_Text imageNameText;
	public GameObject foodDescriptionBackground;
	public TMP_Text foodDescriptionText;
	[TextArea] public string[] foodDescription;
	
	[Header("Result Element")]
	public Button correctAnswer;
	public Image correctAnswerBanner;
	public TMP_Text correctAnswerText;
	public TMP_Text finishScoreText;
	
	[Header("About Image")]
	public Sprite[] questionImage;
	public string[] imageNameThai = {"แอปเปิ้ล", "อโวคาโด", "เบียร์", "แตงโม", "เบคอน", "กล้วย", "องุ่น", "พีช", "ไวน์", "พริกแดง"};
    public string[] imageNameEnglish = { "Apple", "Avocado", "Beer", "Watermelon", "Bacon", "Banana", "Grape", "Peach", "Wine", "Pepper Red" };
    public bool[] isShouldEat;
	
	[Header("Score")]
	public Timer timer;
	public TMP_Text timerText; 
	[SerializeField] private int currentQuestion = 0;
	[SerializeField] private int score = 0;
	public List<string> incorrectFoods = new();
	
	[Header("Analytics")]
	public GoogleFormLogger googleFormLogger;
	[SerializeField] private int viewDescription = 0;
	[SerializeField] private int tapCount = 0;
	private bool isEnd = false;
	
	[Header("Other")]
	public string sceneName;
	
	private void Start()
	{
		correctAnswer.onClick.AddListener(Hide);
		UpdateScore();
		SetImage();
		timer.StartTimer();
	}
	
	private void Update()
	{
		timerText.text = "Time " + timer.GetFormattedTime();
		
		// Check if the device has a touch screen.
		if (Input.touchCount > 0)
		{
			// Loop to check for screen taps
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);

				// Check if it is the first touch (first tap)
				if (touch.phase == TouchPhase.Began)
				{
					tapCount++;
				}
			}
		}
		// Or if you use the mouse in the Editor, you can also count clicks.
		else if (Input.GetMouseButtonDown(0)) // 0 is left click
		{
			tapCount++;
		}
	}
	
	public void ChangeQuestion()
	{
		if (currentQuestion < questionImage.Length)
		{
			currentQuestion++;
		}
		UpdateScore();
	}
	
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
			correctAnswerText.text = "ถูกต้องนะครับ!\n+10 คะแนน";
		}
		else
		{
			GSoundManager.Instance.Play(GSoundManager.GSoundName.Wrong);
			correctAnswer.image.color = new Color32(168, 2, 0, 210);
			correctAnswerText.color = new Color32(255, 0, 0, 255);
			correctAnswerText.text = "ตอบผิด เอาใหม่ๆ!\nไม่ได้คะแนนนะ";
			UpdateIncorrectFoods(currentQuestion);
		}
	}
	
	public void UpdateIncorrectFoods(int orderNumber)
	{
		incorrectFoods.Add(imageNameEnglish[orderNumber]);
	}
	
	public void OpenFoodDescription(bool isOpen)
	{
		if (isOpen)
		{
			viewDescription++;
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
			//SceneManager.LoadScene(sceneName);
			FadeTransition.Instance.FadeOutAndLoadScene(sceneName);
		}
		
		if (!isEnd && currentQuestion >= questionImage.Length)
		{
			// Complete here!
			Debug.Log("There is no more question.");
			GSoundManager.Instance.Play(GSoundManager.GSoundName.ClearGame);
			timer.StopTimer();
			correctAnswer.image.color = new Color32(0, 163, 255, 210);
			correctAnswerText.color = Color.white;
			correctAnswerText.text = $"คุณตอบคำถามหมดแล้ว!.\nคุณได้รับคะแนนทั้งหมด {score}/{questionImage.Length * 10} คะแนน";
			googleFormLogger.SubmitForm(score / 10, incorrectFoods, viewDescription, tapCount, timer.GetFormattedTime());
			
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
	
	// Used in ChangeQuestion()
	private void UpdateScore()
	{
		finishScoreText.text = "เสร็จแล้ว " + currentQuestion + "/" + questionImage.Length + " ข้อ\n" +
		"คุณได้รับ " + score + "/" + questionImage.Length * 10 + " คะแนน";
		StartCoroutine(PopupText(1.0f, 1.15f, 0.25f));
	}
	
	private void SetImage()
	{
		if (currentQuestion >= questionImage.Length) return;
		
		// Set image sprite to [currentProposition] number in array list
		question.sprite = questionImage[currentQuestion];
		imageNameText.text = imageNameThai[currentQuestion];
	}
	
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
}
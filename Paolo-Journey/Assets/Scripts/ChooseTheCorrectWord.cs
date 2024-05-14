using TMPro;
using UnityEngine;

public class ChooseTheCorrectWord : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text checkAnswerText;
    private int score;

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        score = 0;
        scoreText.text = "Score : " + score.ToString();

        checkAnswerText.text = "";
    }

    #region Correct & Wrong answer button
    public void CorrectAnswer()
    {
        // Update score
        score += 100;
        scoreText.text = "Score : " + score.ToString();

        checkAnswerText.text = "";
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong Answer");
        checkAnswerText.text = "You answered wrong. Try choosing another option.";
        // Alert text pop-up
        // Shake camera
    }
    #endregion
}
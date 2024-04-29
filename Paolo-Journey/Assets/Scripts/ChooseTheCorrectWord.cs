using TMPro;
using UnityEngine;

public class ChooseTheCorrectWord : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score;

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        score = 0;
        scoreText.text = "Score : " + score.ToString();
    }

    #region Answer and Wrong button
    public void CorrectAnswer()
    {
        score += 100;
        scoreText.text = "Score : " + score.ToString();
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong Answer");
        // Alert text pop-up
        // Shake camera
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MediaGame : MonoBehaviour
{
    public GameObject correctImage;
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    [SerializeField] private GameObject wrong2;
    [SerializeField] private GameObject wrong3;

    #region Correct & Wrong answer button
    public void CorrectAnswer()
    {
        // Play Sound
        SoundManager.Instance.Play(SoundManager.SoundName.Correct);

        correctImage.SetActive(true);
        answer1.SetActive(false);
        answer2.SetActive(false);
        answer3.SetActive(false);
    }

    public void WrongAnswer(int Button)
    {
        // Play Sound
        SoundManager.Instance.Play(SoundManager.SoundName.Wrong);
        if (Button == 2)
        {
            wrong2.SetActive(true);
        }
        else if(Button == 3)
        {
            wrong3.SetActive(true);
        }
    }
    #endregion
}
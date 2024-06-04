using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }

    public void ButtonPaoloJourney()
    {
        SceneManager.LoadScene("PaoloJourney");
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }

    public void ButtonGameplayManager()
    {
        SceneManager.LoadScene("GameplayManager");
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }

    public void ButtonMedia1()
    {
        CSVWriter.Instance.IncrementInterestNebulizers(CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Media1");
    }

    public void ButtonMedia2()
    {
        CSVWriter.Instance.IncrementInterestDressingWounds(CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Media2");
    }

    public void ButtonMedia3()
    {
        CSVWriter.Instance.IncrementInterestDressingWounds(CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Media3");
    }

    public void ButtonGame1()
    {
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Game1");
    }

    public void ButtonGame2()
    {
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Game2");
    }

    public void ButtonGame3()
    {
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("Game3");
    }

    public void ButtonChooseTheCorrectWord()
    {
        CSVWriter.Instance.IncrementInterestTestChildren(CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SoundManager.Instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene("ChooseTheCorrectWord");
    }

    public void ButtonIQTestMenu()
    {
        CSVWriter.Instance.IncrementInterestIQTest(CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SceneManager.LoadScene("IQTestMenu");
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }
}
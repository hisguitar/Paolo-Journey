using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Button2_6()
    {
        SceneManager.LoadScene("2-6");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }

    public void Button7_16()
    {
        SceneManager.LoadScene("7-16");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }

    public void Button17()
    {
        SceneManager.LoadScene("17+");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }

    public void IQTestMenu()
    {
        CSVWriter.Instance.IncrementInterest("IQTest", CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);

        SceneManager.LoadScene("IQTestMenu");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }
    
}



/*using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public int previousSceneIndex;

    public void Button2_6()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(previousSceneIndex);
        SceneManager.LoadScene("2-6");

        //LoadScene("2-6");
    }

    public void Button7_16()
    {
        //LoadScene("7-16");
    }

    public void Button17()
    {
        //LoadScene("17+");
    }

    public void Back()
    {
        Debug.Log(previousSceneIndex);

        // โหลด scene ล่าสุดที่เราได้บันทึกไว้
        SceneManager.LoadScene(previousSceneIndex);
    }
}
*/

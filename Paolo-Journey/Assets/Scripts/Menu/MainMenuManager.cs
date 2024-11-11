using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingMenuCanvas;

    public void MainGame()
    {
        SceneManager.LoadScene("Guide");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);
    }
    
    public void SettingButton()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingMenuCanvas.gameObject.SetActive(true);
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }
    public void CloseSettingButton()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        settingMenuCanvas.gameObject.SetActive(false);
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }
    
    public void IQTestMenu()
    {
        //CSVWriter.Instance.IncrementInterest("IQTest", CSVWriter.Instance.dailyDataList.statisticalDatas[^1]);
        
        SceneManager.LoadScene("IQTestMenu");
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }
    
    public void ExitGame()
    {
        Debug.Log("Exiting game..."); // แสดงข้อความใน Console เมื่อทำการออกจากเกม (สำหรับการทดสอบใน Editor)
        Application.Quit(); // คำสั่งออกจากแอปพลิเคชัน
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

    }

}

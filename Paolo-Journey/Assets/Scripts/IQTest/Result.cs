using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI IQResult;
    public float iq;
    
    private const string GoogleFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSdU24xM8pQS5YKCri-HBEcN1Mf_tByA76aTfb2f_0Z948AABA/formResponse?pli=1";
    //private const string FormUrlกเก้้กเเ้e = "https://docs.google.com/forms/u/2/d/e/1FAIpQLSeZCQlKt1PhO9OvGA24tHmDu5dhE4SAHguaQkoGoUNKeAEy5A/formResponse?pli=1"; // URL of Google Forms

    
    void Start()
    {
        int age = CanvasManager.age;
        int countScore = CanvasManager.countScore;
        float time = CanvasManager.timeCount;
        
        Debug.Log( "age: " + age);
        Debug.Log("Score: " + countScore);
        Debug.Log("Time: " + time);


        if (age <= 15 || (age >= 61 && age <= 70))
        {
            iq = Mathf.RoundToInt(countScore * 8.5f);
        }
        else if ((age >= 16 && age <= 20) || (age >= 51 && age <= 60))
        {
            iq = Mathf.RoundToInt(countScore * 6.5f);
        }
        else if (age >= 21 && age <= 50)
        {
            iq = Mathf.RoundToInt(countScore * 5.8f);
        }

        if (iq >= 81 && iq <= 144)
        {
            IQResult.text = string.Format("{0}", iq);
        }
        else if (iq <= 80)
        {
            IQResult.text = string.Format("คุณมี IQ น้อยกว่า 80");
        }
        else if (iq >= 145)
        {
            IQResult.text = string.Format("คุณมี IQ มากกว่า 145");
        }
        StartCoroutine(UploadToGoogleSheets(age, countScore, time, iq));

    }
    
    private IEnumerator UploadToGoogleSheets(int age, int score, float time, float iq)
    {
        WWWForm form = new WWWForm();

        // ระบุ Entry IDs ของแต่ละฟิลด์ใน Google Form
        form.AddField("entry.407364072", age.ToString());      // ID สำหรับฟิลด์อายุ
        form.AddField("entry.1443607772", score.ToString());    // ID สำหรับฟิลด์คะแนน
        form.AddField("entry.1699454525", time.ToString("F2")); // ID สำหรับฟิลด์เวลา
        form.AddField("entry.1307413893", iq.ToString());       // ID สำหรับฟิลด์ IQ

        using (UnityWebRequest www = UnityWebRequest.Post(GoogleFormUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data successfully submitted to Google Sheets.");
            }
            else
            {
                Debug.LogError($"Error submitting data: {www.error}");
            }
        }
    }
    
    /*void Start()
    {
        int age = CanvasManager.age;
        Debug.Log( "age: " + age);
        
        int countScore = CanvasManager.countScore;
        
        Debug.Log("Score: " + countScore);
        
        //iq = (countScore * 10.5f) / age;
        iq = Mathf.RoundToInt((countScore * 10.5f) / age);
        IQResult.text = string.Format("{0}", iq);
    }*/
    
}

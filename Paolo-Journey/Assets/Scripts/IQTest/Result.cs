using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System; // สำหรับ Guid

public class Result : MonoBehaviour
{
    public TextMeshProUGUI IQResult;
    public float iq;
    
    private const string GoogleFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSdU24xM8pQS5YKCri-HBEcN1Mf_tByA76aTfb2f_0Z948AABA/formResponse?pli=1";
    //private const string FormUrlกเก้้กเเ้e = "https://docs.google.com/forms/u/2/d/e/1FAIpQLSeZCQlKt1PhO9OvGA24tHmDu5dhE4SAHguaQkoGoUNKeAEy5A/formResponse?pli=1"; // URL of Google Forms
    public static string sessionId; // รหัส Session ไม่ซ้ำ

    private void Awake()
    {
        // สร้างรหัส Session ไม่ซ้ำในตอนเริ่มเกม
        if (string.IsNullOrEmpty(ScoreManager.sessionId))
        {
            sessionId = Guid.NewGuid().ToString(); // ตัวอย่าง UUID
        }
        else
        {
            sessionId = ScoreManager.sessionId;
        }
    }
    void Start()
    {
        int age = CanvasManager.age;
        int countScore = CanvasManager.countScore;
        float time = CanvasManager.timeCount;
        int numAnsed = CanvasManager.answered.Count(a => a);

        Debug.Log( "age: " + age);
        Debug.Log("Score: " + countScore);
        Debug.Log("Time: " + time);
        Debug.Log($"Number of questions answered: {numAnsed}");
        float rawScore = (float)countScore / numAnsed;
        Debug.Log($"rawScore: {rawScore}");

        
        if (age > 12 && age < 16 ||(age >= 61 && age <= 70))
        {
            //iq = Mathf.RoundToInt(countScore * 8.5f);
            iq = Mathf.RoundToInt(rawScore * 250f);

        }
        else if ((age >= 16 && age <= 20) || (age >= 51 && age <= 60))
        {
            iq = Mathf.RoundToInt(rawScore * 214f);
        }
        else if (age >= 21 && age <= 50)
        {
            iq = Mathf.RoundToInt(rawScore * 187.5f);
        }
        else if (age <= 12)
        {
            iq = Mathf.RoundToInt(50 *rawScore + 75f);

        }

        if (numAnsed <= 25 && numAnsed > 10)
        {
            Debug.Log($"IQ: {iq}");
            // Round down to the nearest ten
            //iq = Mathf.FloorToInt(iq / 10) * 10;
            if (iq % 10 > 6)
            {
                iq = Mathf.CeilToInt(iq / 10f) * 10; // ปัดขึ้นถ้าหลักหน่วย > 6
            }
            else
            {
                iq = Mathf.FloorToInt(iq / 10f) * 10; // ปัดลงถ้าหลักหน่วย <= 6
            }
            
            if (iq >= 81 && iq <= 144)
            {
                IQResult.text = string.Format("{0}", iq);
            }
            else if (iq <= 80)
            {
                IQResult.text = string.Format("คุณมี IQ น้อยกว่า 80");
            }
            else if (iq >= 130)
            {
                IQResult.text = string.Format("คุณมี IQ มากกว่า 130");
            }
            StartCoroutine(UploadToGoogleSheets(age, countScore, time, iq));
        }
        else if (numAnsed > 25 || numAnsed <= 10)
        {
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
        Debug.Log($"IQ: {iq}");
    }
    
    private IEnumerator UploadToGoogleSheets(int age, int score, float time, float iq)
    {
        WWWForm form = new WWWForm();

        // ระบุ Entry IDs ของแต่ละฟิลด์ใน Google Form
        form.AddField("entry.250114465", sessionId);
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

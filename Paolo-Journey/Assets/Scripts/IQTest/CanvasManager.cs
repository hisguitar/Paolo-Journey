using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // UI Components
    public List<GameObject> canvases; 
    public Button[] pageButtons; 
    public Button[] ageButtons;
    [SerializeField] public Slider ageSlider;
    [SerializeField] public TextMeshProUGUI ageSliderText;
    public Button sendAnsButtons; 
    public TextMeshProUGUI timerText;
    public Canvas TimerCanvas;
    

    // Game Variables
    public static int age;
    public static int countScore;
    public int[] score;
    public bool[] answered;
    private int currentCanvasIndex = 0; 
    private float timeCount = 0.0f;

    void Start()
    {
        InitializeGame();
        if (ageSlider != null && ageSliderText != null)
        {
            ageSlider.onValueChanged.AddListener((v) =>
            {
                if (ageSlider.value == 70)
                {
                    ageSliderText.text = v.ToString("0  +");

                }
                else
                {
                    ageSliderText.text = v.ToString("0");

                }

                age = (int)ageSlider.value;
            });
        }
    }

    void Update()
    {
        UpdateTimer();
        CheckAllAnswered();
    }

    void InitializeGame()
    {
        // แสดง Canvas แรกเท่านั้น
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(i == currentCanvasIndex);
        }
        answered = new bool[canvases.Count - 2]; // กำหนดค่าเริ่มต้นให้กับตัวแปร answered
        sendAnsButtons.interactable = false;
    }

    void UpdateTimer()
    {
        if (currentCanvasIndex > 0  && currentCanvasIndex <= canvases.Count - 2)
        {
            TimerCanvas.gameObject.SetActive(true);
            timeCount += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeCount);
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
        else if (currentCanvasIndex == 0 || currentCanvasIndex == canvases.Count-1)
        {
            TimerCanvas.gameObject.SetActive(false);
        }
    }

    void CheckAllAnswered()
    {
        if (answered.All(a => a))
        {
            sendAnsButtons.interactable = true;
            ColorBlock colorBlock = sendAnsButtons.colors;
            colorBlock.normalColor = new Color32(0, 109, 171, 255); 
            sendAnsButtons.colors = colorBlock;
        }
    }

    public void Skip()
    {
        SwitchCanvas((currentCanvasIndex + 1) % canvases.Count);
        ChangeInteracButton(currentCanvasIndex-1);
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton2);

    }

    public void Previous()
    {
        currentCanvasIndex--;
        if (currentCanvasIndex < 0)
        {
            currentCanvasIndex = canvases.Count - 1;
        }
        Debug.Log("gdg" + currentCanvasIndex);
        canvases[currentCanvasIndex+1].SetActive(false);

        SwitchCanvas(currentCanvasIndex);
        ChangeInteracButton(currentCanvasIndex-1);
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton2);
        
    }

    public void TrueAns()
    {
        AnswerQuestion(1);
    }
    
    public void FalseAns()
    {
        AnswerQuestion(0);
    }
    public void AnswerQuestion(int answer)
    {
        score[currentCanvasIndex - 1] = answer;
        if (currentCanvasIndex != canvases.Count - 2)
        {
            
            //answered[canvases.Count - 2] = true;
            SwitchCanvas((currentCanvasIndex + 1) % canvases.Count);
        }
        else
        {                
            answered[canvases.Count - 3] = true;

            SetButtonColor(pageButtons[currentCanvasIndex-1], Color.gray);
            if (!answered.All(a => a))
            {
                // หาหน้าที่ผู้ใช้ยังไม่ได้ตอบคำถาม
                int nextUnansweredIndex = Array.IndexOf(answered, false);
                // นำผู้ใช้ไปยังหน้าที่ยังไม่ได้ตอบคำถาม
                ChagePageButton(nextUnansweredIndex + 1);
                Debug.Log("nextUnansweredIndex: " + nextUnansweredIndex);
                
            }
        }

        OnAnswerClick(currentCanvasIndex - 2);
        ChangeInteracButton(currentCanvasIndex);
        answered[currentCanvasIndex - 2] = true;
    }



    void SwitchCanvas(int index)
    {
        canvases[currentCanvasIndex].SetActive(false);
        canvases[index].SetActive(true);
        currentCanvasIndex = index;
    }

    public void OnAnswerClick(int pageIndex)
    {
        SetButtonColor(pageButtons[pageIndex], Color.gray);
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton1);
    }

    void SetButtonColor(Button button, Color color)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        button.colors = colorBlock;
    }

    public void ChagePageButton(int index)
    { 
        SwitchCanvas(index);
        ChangeInteracButton(currentCanvasIndex);
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton2);
    }
    
    /*public void AgeButton(int index)
    { 
        age = index;
        ageButtons[index - 2].interactable = false;
        for (int i = 0; i < ageButtons.Length; i++)
        {
            if (i != index - 2)
            {
                ageButtons[i].interactable = true;
            }
        }
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton1);

    }*/
    public void AgeButton(int index)
    {
        age = index;
        if (index >= 2 && index <= 6)
        {
            ageButtons[index - 2].interactable = false;
            for (int i = 0; i < ageButtons.Length; i++)
            {
                if (i != index - 2)
                {
                    ageButtons[i].interactable = true;
                }
            }
        }
        else if (index >= 7 && index <= 16)
        {
            ageButtons[index - 7].interactable = false;
            for (int i = 0; i < ageButtons.Length; i++)
            {
                if (i != index - 7)
                {
                    ageButtons[i].interactable = true;
                }
            }
        }
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton1);
    }

    public void ChangeInteracButton(int pageIndex)
    {
        for (int i = 0; i < pageButtons.Length; i++)
        {
            if (i == currentCanvasIndex - 1)
            {
                pageButtons[i].interactable = false;
            }
            else
            {
                pageButtons[i].interactable = true;
            }
        }
    }

    public void SendAnsButton()
    {
        SwitchCanvas(canvases.Count-1); // เปลี่ยน currentCanvasIndex เป็นดัชนีของ Canvas หน้าสุดท้าย
        countScore = score.Count(s => s == 1);
        SoundManager.instance.Play(SoundManager.SoundName.ClickButton1);
    }
}


/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // UI Components
    public List<GameObject> canvases; 
    public Button[] pageButtons; 
    public Button[] ageButtons; 
    public Button sendAnsButtons; 
    public TextMeshProUGUI timerText;
    public Canvas TimerCanvas;
    

    // Game Variables
    public static int age;
    public static int countScore;
    public int[] score;
    public bool[] answered;
    private int currentCanvasIndex = 0; 
    private float timeCount = 0.0f;
    
    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        UpdateTimer();
        CheckAllAnswered();
    }

    void InitializeGame()
    {
        // แสดง Canvas แรกเท่านั้น
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(i == currentCanvasIndex);
        }
        answered = new bool[canvases.Count - 2]; // กำหนดค่าเริ่มต้นให้กับตัวแปร answered
        sendAnsButtons.interactable = false;
    }

    void UpdateTimer()
    {
        if (currentCanvasIndex > 0  && currentCanvasIndex <= canvases.Count-1)
        {
            TimerCanvas.gameObject.SetActive(true);
            timeCount += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeCount);
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
        else if (currentCanvasIndex == 0 || currentCanvasIndex == canvases.Count-1)
        {
            TimerCanvas.gameObject.SetActive(false);
        }
    }

    void CheckAllAnswered()
    {
        if (answered.All(a => a))
        {
            sendAnsButtons.interactable = true;
            ColorBlock colorBlock = sendAnsButtons.colors;
            colorBlock.normalColor = new Color32(0, 109, 171, 255); 
            sendAnsButtons.colors = colorBlock;
        }
    }

    public void Skip()
    {
        SwitchCanvas((currentCanvasIndex + 1) % canvases.Count);
    }

    public void Previous()
    {
        currentCanvasIndex--;
        if (currentCanvasIndex < 0)
        {
            currentCanvasIndex = canvases.Count - 1;
        }
        SwitchCanvas(currentCanvasIndex);
    }

    public void TrueAns()
    {
        AnswerQuestion(1);
    }
    
    public void FalseAns()
    {
        AnswerQuestion(0);
    }

    void AnswerQuestion(int answer)
    {
        score[currentCanvasIndex - 1] = answer;
        if (currentCanvasIndex != currentCanvasIndex-1)
        {
            SwitchCanvas((currentCanvasIndex + 1) % canvases.Count);
        }
        else if(currentCanvasIndex == currentCanvasIndex-1)
        {
            SetButtonColor(pageButtons[currentCanvasIndex-1], Color.gray);
            if (!answered.All(a => a))
            {
                // หาหน้าที่ผู้ใช้ยังไม่ได้ตอบคำถาม
                int nextUnansweredIndex = Array.IndexOf(answered, false);
                // นำผู้ใช้ไปยังหน้าที่ยังไม่ได้ตอบคำถาม
                ChagePageButton(nextUnansweredIndex + 1);
                answered[currentCanvasIndex-1] = true;
            }
        }

        OnAnswerClick(currentCanvasIndex - 2);
        ChangeInteracButton(currentCanvasIndex);
        answered[currentCanvasIndex - 2 ] = true;
    }

    void SwitchCanvas(int index)
    {
        canvases[currentCanvasIndex].SetActive(false);
        canvases[index].SetActive(true);
        currentCanvasIndex = index;
    }

    public void OnAnswerClick(int pageIndex)
    {
        SetButtonColor(pageButtons[pageIndex], Color.gray);
    }

    void SetButtonColor(Button button, Color color)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        button.colors = colorBlock;
    }

    public void ChagePageButton(int index)
    { 
        SwitchCanvas(index);
        ChangeInteracButton(currentCanvasIndex);
    }
    
    public void AgeButton(int index)
    { 
        age = index;
        ageButtons[index - 2].interactable = false;
        for (int i = 0; i < ageButtons.Length; i++)
        {
            if (i != index - 2)
            {
                ageButtons[i].interactable = true;
            }
        }
    }

    public void ChangeInteracButton(int pageIndex)
    {
        for (int i = 0; i < pageButtons.Length; i++)
        {
            if (i == currentCanvasIndex - 1)
            {
                pageButtons[i].interactable = false;
            }
            else
            {
                pageButtons[i].interactable = true;
            }
        }
    }

    public void SendAnsButton()
    {
        SwitchCanvas(canvases.Count-1); // เปลี่ยน currentCanvasIndex เป็นดัชนีของ Canvas หน้าสุดท้าย
        countScore = score.Count(s => s == 1);
    }
}
*/
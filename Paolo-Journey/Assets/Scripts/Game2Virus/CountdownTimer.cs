using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // ตัวเลข
    public Image timerCircle; // วงกลม
    public GameObject finishCanvas;
    public GameObject Canvas;
    public bool isGameOver = false;



    public float timeLimit = 50f; // 50 วินาที
    private float currentTime;

    void Start()
    {
        currentTime = timeLimit; // ตั้งค่าเริ่มต้น
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // ลดเวลา
            UpdateTimerUI(); // อัปเดต UI
        }
        else if (currentTime <= 0)
        {
            finishCanvas.SetActive(true);
            Canvas.SetActive(false);
            isGameOver = true;
        }
    }

    void UpdateTimerUI()
    {
        // อัปเดตตัวเลข
        timerText.text = Mathf.CeilToInt(currentTime).ToString();

        // อัปเดตหลอดวงกลม
        timerCircle.fillAmount = currentTime / timeLimit;
    }
}


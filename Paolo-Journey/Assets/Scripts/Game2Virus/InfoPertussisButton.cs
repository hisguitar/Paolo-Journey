using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoPertussisButton : MonoBehaviour
{
    public GameObject pertussisButton;
    public GameObject infoPanels; // Panel ข้อมูลไวรัสท
    public int requiredScore = 500;   // คะแนนที่ต้องการ


    void Start()
    {
        pertussisButton.SetActive(false); // ซ่อนปุ่มเมื่อเริ่มต้น
    }
    
    void Update()
    {
        if (ScoreManager.Instance.score >= requiredScore)
        {
            pertussisButton.SetActive(true);
        } 
    }
    
    public void ShowpPrtussisinfo()
    {
        infoPanels.SetActive(true);
    }
    public void ClosePrtussisinfo()
    {
        infoPanels.SetActive(false);
    }
}

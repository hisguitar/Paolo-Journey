using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public List<GameObject> canvases; // รายการของ Canvas ทั้งหมด
    private int currentCanvasIndex = 0; // ดัชนีของ Canvas ปัจจุบัน
    public int[] score;
    public Button[] pageButtons; // ปุ่มเลือกหน้า
    public Color normalColor; // สีปกติของปุ่ม


    
    private float timeCount = 0.0f;
    public TextMeshProUGUI timerText;
    public Canvas TimerCanvas;

    void Update()
    {
        if (currentCanvasIndex > 0)
        {
            TimerCanvas.gameObject.SetActive(true);
            timeCount += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeCount);
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
        else if (currentCanvasIndex == 0)
        {
            TimerCanvas.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        // แสดง Canvas แรกเท่านั้น
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(i == currentCanvasIndex);
        }
    }

    public void Skip()
    {
        // ปิด Canvas ปัจจุบัน
        canvases[currentCanvasIndex].SetActive(false);

        // ขยับไปยัง Canvas ถัดไป
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Count;

        // เปิด Canvas ถัดไป
        canvases[currentCanvasIndex].SetActive(true);
        ChangeColorButton(currentCanvasIndex);

    }

    public void Previous()
    {
        // ปิด Canvas ปัจจุบัน
        canvases[currentCanvasIndex].SetActive(false);

        // ขยับไปยัง Canvas ก่อนหน้า
        currentCanvasIndex--;
        if (currentCanvasIndex < 0)
        {
            currentCanvasIndex = canvases.Count - 1;
        }

        // เปิด Canvas ก่อนหน้า
        canvases[currentCanvasIndex].SetActive(true);
        ChangeColorButton(currentCanvasIndex);

    }

    public void TrueAns()
    {
        
        Debug.Log(currentCanvasIndex);
        score[currentCanvasIndex - 1] = 1;
        
        // ปิด Canvas ปัจจุบัน
        canvases[currentCanvasIndex].SetActive(false);
        
        // ขยับไปยัง Canvas ถัดไป
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Count;

        // เปิด Canvas ถัดไป
        canvases[currentCanvasIndex].SetActive(true);
        OnAnswerClick(currentCanvasIndex - 2);
        ChangeColorButton(currentCanvasIndex);

    }
    
        public void FalseAns()
    {
        
        Debug.Log(currentCanvasIndex);
        score[currentCanvasIndex - 1] = 0;
        
        // ปิด Canvas ปัจจุบัน
        canvases[currentCanvasIndex].SetActive(false);
        
        // ขยับไปยัง Canvas ถัดไป
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Count;

        // เปิด Canvas ถัดไป
        canvases[currentCanvasIndex].SetActive(true);
        OnAnswerClick(currentCanvasIndex - 2);
        ChangeColorButton(currentCanvasIndex);

    }

    public void OnAnswerClick(int pageIndex)
    {
        // กำหนดสีของปุ่มที่ถูกคลิก
        ColorBlock colorBlock = pageButtons[pageIndex].colors;
        colorBlock.normalColor = Color.gray; // สีที่คุณต้องการ
        pageButtons[pageIndex].colors = colorBlock;
    }

    public void ChagePageButton(int index)
    { 
        canvases[currentCanvasIndex].SetActive(false);
        canvases[index].SetActive(true);
        currentCanvasIndex = index;
        ChangeColorButton(currentCanvasIndex);
        
    }
    
    public void ChangeColorButton(int pageIndex)
    {
        for (int i = 0; i < pageButtons.Length; i++)
        {
            if (i == currentCanvasIndex - 1)
            {
                // ทำให้ปุ่มปัจจุบันไม่สามารถโต้ตอบได้
                pageButtons[i].interactable = false;
            }
            else
            {
                // ทำให้ปุ่มอื่น ๆ สามารถโต้ตอบได้
                pageButtons[i].interactable = true;
            }
        }
        
    }
    

    

}
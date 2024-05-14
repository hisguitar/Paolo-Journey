using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableAnsButtons : MonoBehaviour
{
    public Button[] ansButtons; // ปุ่มทั้งหมดสำหรับแต่ละ canvas

    public void OnAnswerClick(int buttonIndex)
    {
        // ทำให้สีของปุ่มที่ถูกคลิกเข้มขึ้น
        SetButtonColor(ansButtons[buttonIndex], Color.gray);

        // ทำให้สีของปุ่มอื่น ๆ สว่างขึ้น
        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i != buttonIndex)
            {
                SetButtonColor(ansButtons[i], Color.white);
            }
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        button.colors = colorBlock;
    }
    
    
    
    /*public void OnAnswerClick(int buttonIndex)
    {
        // ทำให้ปุ่มที่ถูกคลิกไม่สามารถโต้ตอบได้
        ansButtons[buttonIndex].interactable = false;

        // ทำให้ปุ่มอื่น ๆ สามารถโต้ตอบได้
        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i != buttonIndex)
            {
                ansButtons[i].interactable = true;
            }
        }
    }*/
}




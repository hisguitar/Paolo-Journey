/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanChangeScene : MonoBehaviour
{
    private bool canChangeScene = false; // ใช้ตัวแปรนี้เพื่อตรวจสอบว่ากดเปลี่ยน Scene ได้หรือยัง

    private void Start()
    {
        StartCoroutine(EnableSceneChangeAfterDelay(1.5f)); // เรียก Coroutine เพื่อหน่วงเวลา 1.5 วินาที
    }

    private void Update()
    {
        // ตรวจสอบว่าผู้เล่นแตะหน้าจอ และเปลี่ยน Scene ได้
        if (canChangeScene && Input.GetMouseButtonDown(0))
        {
            ChangeScene();
        }
    }

    // Coroutine สำหรับหน่วงเวลาการเปลี่ยน Scene
    private IEnumerator EnableSceneChangeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // รอเวลาตามที่กำหนด
        canChangeScene = true; // อนุญาตให้เปลี่ยน Scene ได้
    }

    // ฟังก์ชันสำหรับเปลี่ยน Scene
    public void ChangeScene()
    {
        SceneManager.LoadScene("NewGame2Menu");
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }
}*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // สำหรับใช้งาน UI Button

public class CanChangeScene : MonoBehaviour
{
    private bool canChangeScene = false; // ใช้ตัวแปรนี้เพื่อตรวจสอบว่ากดเปลี่ยน Scene ได้หรือยัง

    private void Start()
    {
        StartCoroutine(EnableSceneChangeAfterDelay(1.5f)); // เรียก Coroutine เพื่อหน่วงเวลา 1.5 วินาที
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม
    public void OnChangeSceneButtonPressed()
    {
        if (canChangeScene)
        {
            ChangeScene();
        }
    }

    // Coroutine สำหรับหน่วงเวลาการเปลี่ยน Scene
    private IEnumerator EnableSceneChangeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // รอเวลาตามที่กำหนด
        canChangeScene = true; // อนุญาตให้เปลี่ยน Scene ได้
    }

    // ฟังก์ชันสำหรับเปลี่ยน Scene
    private void ChangeScene()
    {
        SceneManager.LoadScene("NewGame2Menu");
        SoundManager.Instance.Play(SoundManager.SoundName.Click);
    }
}


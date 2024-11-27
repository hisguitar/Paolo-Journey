using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ลาก VideoPlayer เข้ามาใน Inspector
    public GameObject videoCanvas; // ลาก Canvas ที่ใช้แสดงวิดีโอ
    public GameObject mainCanvas;
    public GameObject chatUIController;
    public GameObject chatUI;
    private bool isVideoFinished = false;

    void Start()
    {
        // ตั้งค่าให้ตรวจจับเมื่อวิดีโอเล่นจบ
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ตรวจจับการกดคลิก
        {
            if (isVideoFinished)
            {
                CloseVideoCanvas(); // ปิด Canvas เมื่อจบวิดีโอ
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        isVideoFinished = true; // เปลี่ยนสถานะให้กดได้
    }

    void CloseVideoCanvas()
    {
        videoCanvas.SetActive(false); // ปิด Canvas
        mainCanvas.SetActive(true);
        chatUIController.SetActive(true);
        chatUI.SetActive(true);
    }
}

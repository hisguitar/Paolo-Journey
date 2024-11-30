using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 120; // ล็อก FPS ไว้ที่ 60
        QualitySettings.vSyncCount = 0;  // ปิด VSync
    }
}
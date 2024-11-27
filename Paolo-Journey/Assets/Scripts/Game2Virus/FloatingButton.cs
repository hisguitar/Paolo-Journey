using UnityEngine;

public class RandomFloatingButton : MonoBehaviour
{
    public float speed = 50f; // ความเร็วในการลอย
    public Vector2 bounds = new Vector2(200f, 200f); // ขอบเขตการลอย (X, Y)
    
    private RectTransform rectTransform;
    private Vector2 direction; // ทิศทางการเคลื่อนที่

    void Start()
    {
        // รับ RectTransform ของปุ่ม
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // กำหนดทิศทางเริ่มต้นแบบสุ่ม
            direction = Random.insideUnitCircle.normalized;
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // เคลื่อนที่ปุ่มในทิศทางที่สุ่ม
            rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

            // ตรวจสอบว่าถึงขอบเขตหรือไม่
            Vector2 currentPosition = rectTransform.anchoredPosition;
            if (Mathf.Abs(currentPosition.x) > bounds.x)
            {
                direction.x *= -1; // กลับทิศทางในแกน X
                currentPosition.x = Mathf.Sign(currentPosition.x) * bounds.x; // คงตำแหน่งไว้ในขอบเขต
            }
            if (Mathf.Abs(currentPosition.y) > bounds.y)
            {
                direction.y *= -1; // กลับทิศทางในแกน Y
                currentPosition.y = Mathf.Sign(currentPosition.y) * bounds.y; // คงตำแหน่งไว้ในขอบเขต
            }

            // อัปเดตตำแหน่งของปุ่ม
            rectTransform.anchoredPosition = currentPosition;
        }
    }
}
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 direction;

    private float leftBound = -6.7f;
    private float rightBound = 6.7f;
    private float upperBound = 5f;
    private float lowerBound = -5f;

    private bool isExpanding = false; // สถานะว่ากำลังขยายตัวหรือไม่
    private CountdownTimer countdownTimer;  // ตัวแปรอ้างอิงไปที่ CountdownTimer


    void Start()
    {
        // กำหนดทิศทางเริ่มต้นแบบสุ่ม
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        countdownTimer = FindObjectOfType<CountdownTimer>();
    }

    void Update()
    {
        // เคลื่อนไหวเฉพาะเมื่อไม่ได้อยู่ในสถานะขยายตัว
        if (!isExpanding)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            // ตรวจสอบขอบเขต และสะท้อนทิศทางเมื่อชนขอบ
            if (transform.position.x > rightBound || transform.position.x < leftBound)
                direction.x = -direction.x;

            if (transform.position.y > upperBound || transform.position.y < lowerBound)
                direction.y = -direction.y;
        }
    }

    private void OnMouseDown()
    {
        if (countdownTimer != null && !countdownTimer.isGameOver && !isExpanding) // ป้องกันการกดซ้ำเมื่อกำลังขยายตัว
        {
            isExpanding = true; // ตั้งสถานะว่ากำลังขยายตัว
            StartCoroutine(GrowAndShrinkEffect());
            ScoreManager.Instance.AddScore(1, transform.position); // เพิ่มคะแนน พร้อมส่งตำแหน่งไวรัส
            SoundManager.Instance.Play(SoundManager.SoundName.ClickButton1);

        }
    }

    private System.Collections.IEnumerator GrowAndShrinkEffect()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 maxScale = originalScale * 1.5f; // ขนาดขยายสูงสุด
        float duration = 0.5f; // ระยะเวลาในการขยายและหด

        // ขยายตัว
        float elapsed = 0f;
        while (elapsed < duration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, maxScale, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // หดตัว
        elapsed = 0f;
        while (elapsed < duration / 2)
        {
            transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ลบ GameObject ออกจากฉาก
        Destroy(gameObject);
    }
}

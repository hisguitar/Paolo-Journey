using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text scoreText;
    public TMP_Text totolScoreText;
    public TMP_Text bonusText;
    private int colorIndex = 0; // ดัชนีสำหรับไล่สี
    public VirusSpawner virusSpawner; // ลิงก์กับ VirusSpawner
    public float spawnRate = 0.5f;

    public int score = 0;
    private int comboCount = 0;
    private float lastHitTime = 0f;
    public float comboTimeLimit = 0.3f; // เวลาที่สามารถต่อ Combo ได้
    public float bonusDisplayTime = 0.5f; // เวลาที่แสดงข้อความโบนัส
    public float scaleEffectDuration = 0.05f; // ระยะเวลาการขยายข้อความ
    private Coroutine bonusEffectCoroutine; // เก็บ Coroutine ที่กำลังรันอยู่

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
        bonusText.gameObject.SetActive(false); // ซ่อนข้อความ Bonus ในตอนเริ่ม
    }

    public void AddScore(int points, Vector3 virusPosition)
    {
        float currentTime = Time.time;

        // ตรวจสอบว่า Combo ยังต่อเนื่องหรือไม่
        if (currentTime - lastHitTime <= comboTimeLimit)
        {
            comboCount++;
        }
        else
        {
            comboCount = 1; // รีเซ็ต Combo
            //virusSpawner.UpdateSpawnInterval(0.5f); // รีเซ็ต spawnInterval
            spawnRate = 0.5f;
            virusSpawner.UpdateSpawnInterval(spawnRate); // รีเซ็ต spawnInterval
        }

        lastHitTime = currentTime;

        // คำนวณโบนัสคะแนน
        int bonus = comboCount > 1 ? comboCount - 1 : 0;
        score += points + bonus;

        // แสดงข้อความ Bonus หากมีโบนัส
        if (bonus > 0)
        {
            bonusText.text = $"Bonus x{bonus}!";
            
            colorIndex = (bonus + 1) % rainbowColors.Length; // วนกลับเมื่อถึงสีสุดท้าย
            bonusText.color = rainbowColors[colorIndex]; // ตั้งค่าสีให้ข้อความ
            
            bonusText.gameObject.SetActive(true);
            virusSpawner.UpdateSpawnInterval(comboCount);
            if (spawnRate > 0.1)
            {
                spawnRate -= 0.01f;
            }

            // เรียกใช้เอฟเฟกต์การขยายข้อความ
            if (bonusEffectCoroutine != null)
            {
                StopCoroutine(bonusEffectCoroutine);
            }
            bonusEffectCoroutine = StartCoroutine(HandleBonusEffect());
        }
        virusSpawner.UpdateSpawnInterval(spawnRate); // รีเซ็ต spawnInterval

        UpdateScoreText();
    }
    private Color[] rainbowColors = new Color[]
    {
        // โทนสีสดใสที่คอนทราสต์กัน
        new Color(1f, 0.4f, 0.4f), // แดงสด
        new Color(1f, 0.6f, 0.4f), // ส้มสด
        new Color(1f, 0.9f, 0.4f), // เหลืองสดใส
        new Color(0.6f, 1f, 0.4f), // เขียวสดใส
        new Color(0.4f, 1f, 0.6f), // เขียวมะนาวสดใส
        new Color(0.4f, 0.6f, 1f), // ฟ้าสดใส
        new Color(0.4f, 0.4f, 1f), // น้ำเงินสดใส
        new Color(0.6f, 0.4f, 1f), // ม่วงสดใส
        new Color(1f, 0.4f, 1f), // ชมพูสดใส
        new Color(1f, 0.6f, 0.6f), // แดงอ่อนสดใส
    };
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
        totolScoreText.text = $"Score: {score}";
    }

    private System.Collections.IEnumerator HandleBonusEffect()
    {
        // รีเซ็ต Scale ให้กลับไปขนาดปกติทุกครั้งก่อนเริ่ม
        Vector3 originalScale = Vector3.one; 
        bonusText.transform.localScale = originalScale;

        // ขยายข้อความ
        Vector3 targetScale = originalScale * 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < scaleEffectDuration)
        {
            bonusText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleEffectDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        bonusText.transform.localScale = targetScale;

        // รอก่อนกลับสู่ขนาดเดิม
        elapsedTime = 0f;
        while (elapsedTime < scaleEffectDuration)
        {
            bonusText.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleEffectDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        bonusText.transform.localScale = originalScale;

        // แสดงข้อความจนกว่าครบเวลา
        yield return new WaitForSeconds(bonusDisplayTime);
        bonusText.gameObject.SetActive(false);
        spawnRate = 0.5f;
        virusSpawner.UpdateSpawnInterval(spawnRate); // รีเซ็ต spawnInterval
    }
}

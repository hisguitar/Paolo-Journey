using UnityEngine;
using UnityEngine.SceneManagement;

public class Coronavirus : MonoBehaviour
{
    public GameObject miniCoronavirusPrefab; // Prefab ของลูกไวรัส
    public int minSpawnCount = 2; // จำนวนลูกไวรัสขั้นต่ำ
    public int maxSpawnCount = 4; // จำนวนลูกไวรัสสูงสุด
    public float spawnRadius = 0.5f; // ระยะกระจายรอบๆ ตัวไวรัสหลัก

    private void OnEnable()
    {
        // ฟังเหตุการณ์เมื่อ Scene เปลี่ยนแปลง
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // เลิกฟังเหตุการณ์เมื่อ Scene เปลี่ยนแปลง
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // เมื่อ Scene เปลี่ยนแปลง รีเซ็ตค่าหรือหยุดการขยายตัวที่เกิดขึ้นก่อนหน้า
        StopAllCoroutines(); // หยุดการขยายตัวหากกำลังทำงานอยู่
        transform.localScale = Vector3.one; // รีเซ็ตขนาดให้เป็นปกติ
    }

    // ฟังก์ชันสำหรับแพร่ลูกไวรัส
    public void SpreadMiniCoronavirus()
    {
        // สุ่มจำนวนลูกไวรัส
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            // สร้างตำแหน่งสุ่มในรัศมีรอบตัวไวรัสหลัก
            Vector3 spawnPosition = transform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);

            // สร้างลูกไวรัส
            GameObject miniVirus = Instantiate(miniCoronavirusPrefab, spawnPosition, Quaternion.identity);

            // ตั้งค่าเพิ่มเติมให้ miniCoronavirus หากจำเป็น
            miniVirus.GetComponent<Rigidbody2D>()?.AddForce(Random.insideUnitCircle * 2f, ForceMode2D.Impulse);
        }
    }

    // ตัวอย่างการเรียกใช้ SpreadMiniCoronavirus
    private void OnDestroy()
    {
        // เมื่อไวรัสหลักถูกทำลาย จะปล่อยลูกไวรัส
        SpreadMiniCoronavirus();
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VirusSpawner : MonoBehaviour
{
    public GameObject[] virusPrefabs; // รายการ Prefab เชื้อโรค 4 แบบ
    public float spawnInterval = 0.5f;

    private Vector2 screenBounds;
    public float margin = 0.5f; // ระยะขอบที่ลดลง
    //private float minimumInterval = 0.1f; // ค่า spawnInterval ต่ำสุด
    public float spawnSpeedupFactor = 0.01f; // อัตราการลด spawnInterval

    void Start()
    {
        // คำนวณขอบเขตหน้าจอ และลดระยะขอบ
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        screenBounds -= new Vector2(margin, margin); // ลดระยะขอบ

        InvokeRepeating("SpawnVirus", 1f, spawnInterval);
    }

    void SpawnVirus()
    {
        // สุ่มตำแหน่งในขอบเขตหน้าจอ (หลังลดระยะขอบ)
        float x = Random.Range(-screenBounds.x, screenBounds.x);
        float y = Random.Range(-screenBounds.y, screenBounds.y);
        Vector3 spawnPosition = new Vector3(x, y, 0);

        // สุ่มเลือก Prefab เชื้อโรค
        int randomIndex = Random.Range(0, virusPrefabs.Length);
        GameObject virusPrefab = virusPrefabs[randomIndex];

        Instantiate(virusPrefab, spawnPosition, Quaternion.identity);
    }
    
    /*public void UpdateSpawnInterval(int comboCount)
    {
        // ลด spawnInterval เมื่อคอมโบเพิ่มขึ้น
        spawnInterval = Mathf.Max(minimumInterval, spawnInterval - spawnSpeedupFactor);
        
        // อัปเดตการเรียก Spawn ใหม่ด้วย interval ใหม่
        CancelInvoke("SpawnVirus");
        InvokeRepeating("SpawnVirus", 0f, spawnInterval);
    }*/
    public void UpdateSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
        CancelInvoke("SpawnVirus");
        InvokeRepeating("SpawnVirus", 0f, spawnInterval);
    }

}

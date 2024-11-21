using UnityEngine;

public class VirusInfoManager : MonoBehaviour
{
    public GameObject[] viruses; // ปุ่มไวรัสทั้งหมด
    public GameObject[] infoPanels; // Panel ข้อมูลไวรัสทั้งหมด

    // ฟังก์ชันสำหรับแสดงข้อมูลไวรัสตาม index
    public void ShowVirusInfo(int virusIndex)
    {
        // ซ่อน Panel ข้อมูลทั้งหมดก่อน
        foreach (GameObject panel in infoPanels)
        {
            panel.SetActive(false);
        }

        // แสดง Panel ที่ตรงกับไวรัสที่กด
        if (virusIndex >= 0 && virusIndex < infoPanels.Length)
        {
            infoPanels[virusIndex].SetActive(true);
        }
    }

    // ฟังก์ชันสำหรับซ่อนข้อมูลไวรัสทั้งหมด
    public void OnMouseDown()
    {
        foreach (GameObject panel in infoPanels)
        {
            panel.SetActive(false);
        }
    }
}